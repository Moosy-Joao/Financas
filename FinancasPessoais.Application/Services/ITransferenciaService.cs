using FinancasPessoais.Application.DTOs;
using FinancasPessoais.Domain.Entities;

namespace FinancasPessoais.Application.Services;

public interface ITransferenciaService
{
    Task<Transferencia> CriarAsync(TransferenciaDto dto);
    Task EstornarAsync(Guid transferenciaId);
}