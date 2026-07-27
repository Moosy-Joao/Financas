using FinancasPessoais.Domain.Enums;

namespace FinancasPessoais.Domain.Entities;

public class ContaFinanceira : Entity
{
    public string Nome { get; set; } = string.Empty;
    public TipoConta Tipo { get; set; }
    public string? Instituicao { get; set; }
    public decimal SaldoInicial { get; set; }
    public bool Ativo { get; set; } = true;
    public bool Arquivado { get; set; }

    public ICollection<Movimentacao> Movimentacoes { get; set; } = new List<Movimentacao>();
}