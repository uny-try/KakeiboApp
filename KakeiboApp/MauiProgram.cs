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
		builder.Services.AddSingleton<EditTransactionViewModel>();
		builder.Services.AddSingleton<ITransactionRepository, TransactionRepository>();
		builder.Services.AddSingleton<INavigationService, NavigationService>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
