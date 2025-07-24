using Microsoft.Extensions.Logging;

using KakeiboApp.Core.Interfaces;
using KakeiboApp.Core.Services;
using KakeiboApp.ViewModels;
using KakeiboApp.Services;

namespace KakeiboApp;

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

        builder.Services.AddSingleton<TransactionViewModel>();
        builder.Services.AddTransient<EditTransactionViewModel>();
        builder.Services.AddSingleton<AccountSummaryViewModel>();
        builder.Services.AddSingleton<ITransactionRepository>(
            _ => new FileTransactionRepository(FileSystem.AppDataDirectory));
        builder.Services.AddSingleton<ICategoryRepository>(
            _ => new FileCategoryRepository(FileSystem.AppDataDirectory)
        );
        builder.Services.AddSingleton<IAccountRepository>(
            _ => new FileAccountRepository(FileSystem.AppDataDirectory)
        );
        builder.Services.AddSingleton<INavigationService, NavigationService>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
