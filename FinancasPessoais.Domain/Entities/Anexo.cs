namespace FinancasPessoais.Domain.Entities;

public class Anexo : Entity
{
    public string NomeArquivo { get; set; } = string.Empty;
    public string CaminhoInterno { get; set; } = string.Empty;
    public string? TipoMime { get; set; }
    public long TamanhoBytes { get; set; }

    public Guid? MovimentacaoId { get; set; }
    public Movimentacao? Movimentacao { get; set; }

    public Guid? PagamentoId { get; set; }
    public Pagamento? Pagamento { get; set; }
}