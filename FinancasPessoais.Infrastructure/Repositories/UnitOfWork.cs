using FinancasPessoais.Domain.Entities;
using FinancasPessoais.Domain.Interfaces;
using FinancasPessoais.Infrastructure.Data;

namespace FinancasPessoais.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private IMovimentacaoRepository? _movimentacoes;
    private IRepository<ContaFinanceira>? _contas;
    private IRepository<Categoria>? _categorias;
    private IRepository<Pessoa>? _pessoas;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public IMovimentacaoRepository Movimentacoes => _movimentacoes ??= new MovimentacaoRepository(_context);
    public IRepository<ContaFinanceira> Contas => _contas ??= new Repository<ContaFinanceira>(_context);
    public IRepository<Categoria> Categorias => _categorias ??= new Repository<Categoria>(_context);
    public IRepository<Pessoa> Pessoas => _pessoas ??= new Repository<Pessoa>(_context);

    public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

    public void Dispose() => _context.Dispose();
}