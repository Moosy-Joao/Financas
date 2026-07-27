using FinancasPessoais.Application.Services;
using FinancasPessoais.Domain.Interfaces;
using FinancasPessoais.Infrastructure.Data;
using FinancasPessoais.Infrastructure.Repositories;
using FinancasPessoais.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FinancasPessoais.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string dbPath)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite($"Data Source={dbPath}"));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IMovimentacaoService, MovimentacaoService>();
        services.AddScoped<IPainelService, PainelService>();

        return services;
    }
}