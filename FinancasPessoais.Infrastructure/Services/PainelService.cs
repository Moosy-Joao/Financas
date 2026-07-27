using FinancasPessoais.Application.Services;
using FinancasPessoais.Domain.Enums;
using FinancasPessoais.Domain.Interfaces;

namespace FinancasPessoais.Infrastructure.Services;

public class PainelService : IPainelService
{
    private readonly IUnitOfWork _unitOfWork;

    public PainelService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PainelResumoDto> ObterResumoAsync(DateTime mesReferencia)
    {
        var inicio = new DateTime(mesReferencia.Year, mesReferencia.Month, 1);
        var fim = inicio.AddMonths(1).AddDays(-1);

        var contas = await _unitOfWork.Contas.GetAllAsync();
        decimal saldoRealizado = 0;
        decimal saldoPrevisto = 0;

        foreach (var conta in contas.Where(c => c.Ativo && !c.Arquivado))
        {
            saldoRealizado += await _unitOfWork.Movimentacoes.GetSaldoRealizadoAsync(conta.Id);
            saldoPrevisto += await _unitOfWork.Movimentacoes.GetSaldoPrevistoAsync(conta.Id);
        }

        var totalReceitas = await _unitOfWork.Movimentacoes.GetTotalPorTipoAsync(TipoMovimentacao.Receita, inicio, fim);
        var totalDespesas = await _unitOfWork.Movimentacoes.GetTotalPorTipoAsync(TipoMovimentacao.Despesa, inicio, fim);

        var despesasPendentes = await _unitOfWork.Movimentacoes.GetByPeriodoAsync(inicio, fim);
        var totalDespesasPendentes = despesasPendentes
            .Where(m => m.Tipo == TipoMovimentacao.Despesa && m.Situacao != SituacaoMovimentacao.Paga && m.Situacao != SituacaoMovimentacao.Cancelada)
            .Sum(m => m.Valor);

        var hoje = DateTime.Today;
        var vencimentosHoje = despesasPendentes.Count(m => m.Vencimento?.Date == hoje);
        var atrasadas = despesasPendentes.Count(m => m.Vencimento?.Date < hoje && m.Situacao != SituacaoMovimentacao.Paga && m.Situacao != SituacaoMovimentacao.Cancelada);

        return new PainelResumoDto
        {
            SaldoTotalRealizado = saldoRealizado,
            SaldoTotalPrevisto = saldoPrevisto,
            TotalReceitas = totalReceitas,
            TotalDespesas = totalDespesas,
            TotalDespesasPendentes = totalDespesasPendentes,
            QuantidadeVencimentosHoje = vencimentosHoje,
            QuantidadeAtrasadas = atrasadas
        };
    }
}