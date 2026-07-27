namespace FinancasPessoais.Domain.Entities;

public class Pessoa : Entity
{
    public string Nome { get; set; } = string.Empty;
    public bool Ativo { get; set; } = true;

    public ICollection<Movimentacao> Movimentacoes { get; set; } = new List<Movimentacao>();
}