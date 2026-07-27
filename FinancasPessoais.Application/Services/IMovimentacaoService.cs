using FinancasPessoais.Application.DTOs;
using FinancasPessoais.Domain.Entities;

namespace FinancasPessoais.Application.Services;

public interface IMovimentacaoService
{
    Task<Movimentacao> CriarAsync(CriarMovimentacaoDto dto);
    Task RegistrarPagamentoAsync(RegistrarPagamentoDto dto);
    Task EstornarPagamentoAsync(Guid pagamentoId);
    Task CancelarMovimentacaoAsync(Guid movimentacaoId);
}