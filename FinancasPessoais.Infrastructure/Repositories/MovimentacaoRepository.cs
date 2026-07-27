using FinancasPessoais.Domain.Entities;
using FinancasPessoais.Domain.Enums;
using FinancasPessoais.Domain.Interfaces;
using FinancasPessoais.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FinancasPessoais.Infrastructure.Repositories;

public class MovimentacaoRepository : Repository<Movimentacao>, IMovimentacaoRepository
{
    public MovimentacaoRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Movimentacao>> GetByPeriodoAsync(DateTime inicio, DateTime fim)
    {
        return await _context.Movimentacoes
            .Include(m => m.Categoria)
            .Include(m => m.Conta)
            .Include(m => m.Pagamentos)
            .Where(m => m.Data >= inicio && m.Data <= fim && m.ExcluidoEm == null)
            .OrderByDescending(m => m.Data)
            .ToListAsync();
    }

    public async Task<IEnumerable<Movimentacao>> GetByContaAsync(Guid contaId)
    {
        return await _context.Movimentacoes
            .Include(m => m.Categoria)
            .Include(m => m.Pagamentos)
            .Where(m => m.ContaId == contaId && m.ExcluidoEm == null)
            .OrderByDescending(m => m.Data)
            .ToListAsync();
    }

    public async Task<IEnumerable<Movimentacao>> GetByCategoriaAsync(Guid categoriaId)
    {
        return await _context.Movimentacoes
            .Where(m => m.CategoriaId == categoriaId && m.ExcluidoEm == null)
            .OrderByDescending(m => m.Data)
            .ToListAsync();
    }

    public async Task<decimal> GetTotalPorTipoAsync(TipoMovimentacao tipo, DateTime inicio, DateTime fim)
    {
        return await _context.Movimentacoes
            .Where(m => m.Tipo == tipo && m.Data >= inicio && m.Data <= fim && m.ExcluidoEm == null)
            .SumAsync(m => m.Valor);
    }

    public async Task<decimal> GetSaldoRealizadoAsync(Guid contaId)
    {
        var conta = await _context.ContasFinanceiras.FindAsync(contaId);
        if (conta == null) return 0;

        var receitasRecebidas = await _context.Movimentacoes
            .Where(m => m.ContaId == contaId && m.Tipo == TipoMovimentacao.Receita && m.Situacao == SituacaoMovimentacao.Paga && m.ExcluidoEm == null)
            .SumAsync(m => m.Valor);

        var despesasPagas = await _context.Movimentacoes
            .Where(m => m.ContaId == contaId && m.Tipo == TipoMovimentacao.Despesa && m.Situacao == SituacaoMovimentacao.Paga && m.ExcluidoEm == null)
            .SumAsync(m => m.Valor);

        // Transferências enviadas (saída)
        var transferenciasEnviadas = await _context.Transferencias
            .Where(t => t.ContaOrigemId == contaId && t.ExcluidoEm == null)
            .SumAsync(t => t.Valor + (t.Tarifa ?? 0));

        // Transferências recebidas (entrada)
        var transferenciasRecebidas = await _context.Transferencias
            .Where(t => t.ContaDestinoId == contaId && t.ExcluidoEm == null)
            .SumAsync(t => t.Valor);

        return conta.SaldoInicial + receitasRecebidas - despesasPagas - transferenciasEnviadas + transferenciasRecebidas;
    }

    public async Task<decimal> GetSaldoPrevistoAsync(Guid contaId)
    {
        var saldoRealizado = await GetSaldoRealizadoAsync(contaId);

        var receitasPendentes = await _context.Movimentacoes
            .Where(m => m.ContaId == contaId && m.Tipo == TipoMovimentacao.Receita
                && m.Situacao != SituacaoMovimentacao.Paga
                && m.Situacao != SituacaoMovimentacao.Cancelada
                && m.ExcluidoEm == null)
            .SumAsync(m => m.Valor);

        var despesasPendentes = await _context.Movimentacoes
            .Where(m => m.ContaId == contaId && m.Tipo == TipoMovimentacao.Despesa
                && m.Situacao != SituacaoMovimentacao.Paga
                && m.Situacao != SituacaoMovimentacao.Cancelada
                && m.ExcluidoEm == null)
            .SumAsync(m => m.Valor);

        return saldoRealizado + receitasPendentes - despesasPendentes;
    }
}