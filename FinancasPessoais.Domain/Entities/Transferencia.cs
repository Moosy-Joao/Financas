namespace FinancasPessoais.Domain.Entities;

public class Transferencia : Entity
{
    public string Descricao { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public decimal? Tarifa { get; set; }
    public DateTime Data { get; set; }
    public string? Observacao { get; set; }

    public Guid ContaOrigemId { get; set; }
    public ContaFinanceira ContaOrigem { get; set; } = null!;

    public Guid ContaDestinoId { get; set; }
    public ContaFinanceira ContaDestino { get; set; } = null!;

    // Gera duas movimentacoes vinculadas
    public Guid? MovimentacaoSaidaId { get; set; }
    public Movimentacao? MovimentacaoSaida { get; set; }

    public Guid? MovimentacaoEntradaId { get; set; }
    public Movimentacao? MovimentacaoEntrada { get; set; }
}