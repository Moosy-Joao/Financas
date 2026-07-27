namespace FinancasPessoais.Application.Services;

public interface IPainelService
{
    Task<PainelResumoDto> ObterResumoAsync(DateTime mesReferencia);
}

public class PainelResumoDto
{
    public decimal SaldoTotalRealizado { get; set; }
    public decimal SaldoTotalPrevisto { get; set; }
    public decimal TotalReceitas { get; set; }
    public decimal TotalDespesas { get; set; }
    public decimal TotalDespesasPendentes { get; set; }
    public int QuantidadeVencimentosHoje { get; set; }
    public int QuantidadeAtrasadas { get; set; }
}