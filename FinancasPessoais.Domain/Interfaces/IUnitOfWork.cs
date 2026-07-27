using FinancasPessoais.Domain.Entities;

namespace FinancasPessoais.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IMovimentacaoRepository Movimentacoes { get; }
    IRepository<ContaFinanceira> Contas { get; }
    IRepository<Categoria> Categorias { get; }
    IRepository<Pessoa> Pessoas { get; }
    Task<int> SaveChangesAsync();
}