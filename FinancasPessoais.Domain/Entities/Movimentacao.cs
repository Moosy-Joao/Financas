using FinancasPessoais.Domain.Enums;

namespace FinancasPessoais.Domain.Entities;

public class Movimentacao : Entity
{
    public string Descricao { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public DateTime Data { get; set; }
    public DateTime Competencia { get; set; }
    public DateTime? Vencimento { get; set; }
    public TipoMovimentacao Tipo { get; set; }
    public SituacaoMovimentacao Situacao { get; set; } = SituacaoMovimentacao.Pendente;
    public string? Observacao { get; set; }

    public Guid CategoriaId { get; set; }
    public Categoria Categoria { get; set; } = null!;

    public Guid ContaId { get; set; }
    public ContaFinanceira Conta { get; set; } = null!;

    public Guid? PessoaId { get; set; }
    public Pessoa? Pessoa { get; set; }

    public ICollection<Pagamento> Pagamentos { get; set; } = new List<Pagamento>();
    public ICollection<Tag> Tags { get; set; } = new List<Tag>();
    public ICollection<Anexo> Anexos { get; set; } = new List<Anexo>();

    public Guid? TransferenciaRelacionadaId { get; set; }
    public Movimentacao? TransferenciaRelacionada { get; set; }
}