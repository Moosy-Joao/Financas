namespace FinancasPessoais.Application.DTOs;

public class TransferenciaDto  // <-- adicionar 'public'
{
    public string Descricao { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public decimal? Tarifa { get; set; }
    public DateTime Data { get; set; }
    public Guid ContaOrigemId { get; set; }
    public Guid ContaDestinoId { get; set; }
    public string? Observacao { get; set; }
}