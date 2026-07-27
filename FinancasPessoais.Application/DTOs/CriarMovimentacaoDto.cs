using FinancasPessoais.Domain.Enums;

namespace FinancasPessoais.Application.DTOs;

public class CriarMovimentacaoDto  // <-- adicionar 'public'
{
    public string Descricao { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public DateTime Data { get; set; }
    public DateTime Competencia { get; set; }
    public DateTime? Vencimento { get; set; }
    public TipoMovimentacao Tipo { get; set; }
    public Guid CategoriaId { get; set; }
    public Guid ContaId { get; set; }
    public Guid? PessoaId { get; set; }
    public string? Observacao { get; set; }
    public List<string>? Tags { get; set; }
}