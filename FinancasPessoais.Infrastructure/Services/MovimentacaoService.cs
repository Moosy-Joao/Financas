using FinancasPessoais.Application.DTOs;
using FinancasPessoais.Application.Services;
using FinancasPessoais.Domain.Entities;
using FinancasPessoais.Domain.Enums;
using FinancasPessoais.Domain.Interfaces;

namespace FinancasPessoais.Infrastructure.Services;

public class MovimentacaoService : IMovimentacaoService
{
    private readonly IUnitOfWork _unitOfWork;

    public MovimentacaoService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Movimentacao> CriarAsync(CriarMovimentacaoDto dto)
    {
        if (dto.Valor <= 0)
            throw new ArgumentException("Valor deve ser maior que zero.");

        var movimentacao = new Movimentacao
        {
            Descricao = dto.Descricao,
            Valor = dto.Valor,
            Data = dto.Data,
            Competencia = dto.Competencia,
            Vencimento = dto.Vencimento,
            Tipo = dto.Tipo,
            CategoriaId = dto.CategoriaId,
            ContaId = dto.ContaId,
            PessoaId = dto.PessoaId,
            Observacao = dto.Observacao,
            Situacao = dto.Vencimento.HasValue && dto.Vencimento.Value < DateTime.Today
                ? SituacaoMovimentacao.Atrasada
                : SituacaoMovimentacao.Pendente
        };

        // Se for receita com data de hoje ou passada, já marca como recebida
        if (dto.Tipo == TipoMovimentacao.Receita && dto.Data <= DateTime.Today)
        {
            movimentacao.Situacao = SituacaoMovimentacao.Paga;
            await _unitOfWork.Movimentacoes.AddAsync(movimentacao);
            await _unitOfWork.SaveChangesAsync();

            // Cria pagamento automático para receita
            var pagamento = new Pagamento
            {
                MovimentacaoId = movimentacao.Id,
                Valor = dto.Valor,
                DataPagamento = dto.Data,
                ContaId = dto.ContaId
            };
            await _unitOfWork.SaveChangesAsync(); // Já está no contexto? Não, precisa adicionar
        }
        else
        {
            await _unitOfWork.Movimentacoes.AddAsync(movimentacao);
            await _unitOfWork.SaveChangesAsync();
        }

        return movimentacao;
    }

    public async Task RegistrarPagamentoAsync(RegistrarPagamentoDto dto)
    {
        var movimentacao = await _unitOfWork.Movimentacoes.GetByIdAsync(dto.MovimentacaoId);
        if (movimentacao == null) throw new InvalidOperationException("Movimentação não encontrada.");

        if (movimentacao.Situacao == SituacaoMovimentacao.Cancelada)
            throw new InvalidOperationException("Não é possível pagar uma movimentação cancelada.");

        var valorFinal = dto.Valor + (dto.Juros ?? 0) + (dto.Multa ?? 0) - (dto.Desconto ?? 0);

        var pagamento = new Pagamento
        {
            MovimentacaoId = dto.MovimentacaoId,
            Valor = valorFinal,
            Juros = dto.Juros,
            Multa = dto.Multa,
            Desconto = dto.Desconto,
            DataPagamento = dto.DataPagamento,
            ContaId = dto.ContaId,
            Observacao = dto.Observacao
        };

        await _unitOfWork.SaveChangesAsync(); // Precisa adicionar o pagamento ao contexto

        // Atualiza situação
        var totalPago = movimentacao.Pagamentos.Sum(p => p.Valor);
        if (totalPago >= movimentacao.Valor)
            movimentacao.Situacao = SituacaoMovimentacao.Paga;
        else
            movimentacao.Situacao = SituacaoMovimentacao.ParcialmentePaga;

        movimentacao.AtualizadoEm = DateTime.UtcNow;
        await _unitOfWork.SaveChangesAsync();
    }

    public Task EstornarPagamentoAsync(Guid pagamentoId)
    {
        // Implementação futura
        throw new NotImplementedException();
    }

    public async Task CancelarMovimentacaoAsync(Guid movimentacaoId)
    {
        var movimentacao = await _unitOfWork.Movimentacoes.GetByIdAsync(movimentacaoId);
        if (movimentacao == null) throw new InvalidOperationException("Movimentação não encontrada.");

        movimentacao.Situacao = SituacaoMovimentacao.Cancelada;
        movimentacao.AtualizadoEm = DateTime.UtcNow;
        await _unitOfWork.SaveChangesAsync();
    }
}