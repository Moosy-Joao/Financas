using FinancasPessoais.Application.Services;
using FinancasPessoais.Infrastructure;
using Microsoft.Extensions.Logging;

namespace FinancasPessoais.App;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "financas.db");

        builder.Services.AddInfrastructure(dbPath);

        builder.Logging.AddDebug();

        return builder.Build();
    }
}