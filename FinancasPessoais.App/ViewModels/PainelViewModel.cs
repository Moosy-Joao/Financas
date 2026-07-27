using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinancasPessoais.Application.Services;

namespace FinancasPessoais.App.ViewModels;

public partial class PainelViewModel : ObservableObject
{
    private readonly IPainelService _painelService;

    [ObservableProperty]
    private decimal _saldoRealizado;

    [ObservableProperty]
    private decimal _saldoPrevisto;

    [ObservableProperty]
    private decimal _totalReceitas;

    [ObservableProperty]
    private decimal _totalDespesas;

    [ObservableProperty]
    private int _vencimentosHoje;

    [ObservableProperty]
    private int _atrasadas;

    public PainelViewModel(IPainelService painelService)
    {
        _painelService = painelService;
    }

    [RelayCommand]
    private async Task CarregarPainel()
    {
        var resumo = await _painelService.ObterResumoAsync(DateTime.Today);
        SaldoRealizado = resumo.SaldoTotalRealizado;
        SaldoPrevisto = resumo.SaldoTotalPrevisto;
        TotalReceitas = resumo.TotalReceitas;
        TotalDespesas = resumo.TotalDespesas;
        VencimentosHoje = resumo.QuantidadeVencimentosHoje;
        Atrasadas = resumo.QuantidadeAtrasadas;
    }
}