namespace FinancasPessoais.Application.DTOs;

public class RegistrarPagamentoDto  // <-- adicionar 'public'
{
    public Guid MovimentacaoId { get; set; }
    public decimal Valor { get; set; }
    public decimal? Juros { get; set; }
    public decimal? Multa { get; set; }
    public decimal? Desconto { get; set; }
    public DateTime DataPagamento { get; set; }
    public Guid ContaId { get; set; }
    public string? Observacao { get; set; }
}