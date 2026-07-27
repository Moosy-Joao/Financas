using FinancasPessoais.Domain.Enums;

namespace FinancasPessoais.Domain.Entities;

public class Categoria : Entity
{
    public string Nome { get; set; } = string.Empty;
    public string? Cor { get; set; }
    public string? Icone { get; set; }
    public TipoMovimentacao TipoPadrao { get; set; } = TipoMovimentacao.Despesa;
    public Guid? CategoriaPaiId { get; set; }
    public Categoria? CategoriaPai { get; set; }
    public ICollection<Categoria> Subcategorias { get; set; } = new List<Categoria>();
    public ICollection<Movimentacao> Movimentacoes { get; set; } = new List<Movimentacao>();
}