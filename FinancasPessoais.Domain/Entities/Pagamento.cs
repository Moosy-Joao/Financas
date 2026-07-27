namespace FinancasPessoais.Domain.Entities;

public class Pagamento : Entity
{
    public decimal Valor { get; set; }
    public decimal? Juros { get; set; }
    public decimal? Multa { get; set; }
    public decimal? Desconto { get; set; }
    public DateTime DataPagamento { get; set; }
    public string? Observacao { get; set; }

    public Guid MovimentacaoId { get; set; }
    public Movimentacao Movimentacao { get; set; } = null!;

    public Guid ContaId { get; set; }
    public ContaFinanceira Conta { get; set; } = null!;

    public ICollection<Anexo> Anexos { get; set; } = new List<Anexo>();
}