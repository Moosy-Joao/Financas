using FinancasPessoais.Domain.Entities;
using FinancasPessoais.Domain.Enums;

namespace FinancasPessoais.Domain.Interfaces;

public interface IMovimentacaoRepository : IRepository<Movimentacao>
{
    Task<IEnumerable<Movimentacao>> GetByPeriodoAsync(DateTime inicio, DateTime fim);
    Task<IEnumerable<Movimentacao>> GetByContaAsync(Guid contaId);
    Task<IEnumerable<Movimentacao>> GetByCategoriaAsync(Guid categoriaId);
    Task<decimal> GetTotalPorTipoAsync(TipoMovimentacao tipo, DateTime inicio, DateTime fim);
    Task<decimal> GetSaldoRealizadoAsync(Guid contaId);
    Task<decimal> GetSaldoPrevistoAsync(Guid contaId);
}