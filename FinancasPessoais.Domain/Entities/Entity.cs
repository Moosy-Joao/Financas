namespace FinancasPessoais.Domain.Entities;

public abstract class Entity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;  // <-- remover 'protected'
    public DateTime? AtualizadoEm { get; set; }
    public DateTime? ExcluidoEm { get; set; }
    public string Origem { get; set; } = "Manual";
}