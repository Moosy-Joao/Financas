namespace FinancasPessoais.Domain.Entities;

public class Tag : Entity
{
    public string Nome { get; set; } = string.Empty;
    public ICollection<Movimentacao> Movimentacoes { get; set; } = new List<Movimentacao>();
}