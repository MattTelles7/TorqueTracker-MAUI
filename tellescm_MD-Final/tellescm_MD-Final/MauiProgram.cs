using Microsoft.Extensions.Logging;
using tellescm_MD_Final.Data;
using tellescm_MD_Final.Repositories;
using tellescm_MD_Final.ViewModels;
using tellescm_MD_Final.Views;

namespace tellescm_MD_Final;

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

        builder.Services.AddSingleton<DatabaseService>();
        builder.Services.AddSingleton<IVehicleRepository, SQLiteVehicleRepository>();

        builder.Services.AddSingleton<DashboardViewModel>();
        builder.Services.AddSingleton<VehiclesViewModel>();
        builder.Services.AddTransient<AddVehicleViewModel>();

        builder.Services.AddSingleton<DashboardPage>();
        builder.Services.AddSingleton<VehiclesPage>();
        builder.Services.AddTransient<AddVehiclePage>();
        builder.Services.AddSingleton<HistoryPage>();
        builder.Services.AddSingleton<SettingsPage>();
        builder.Services.AddSingleton<AppShell>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
