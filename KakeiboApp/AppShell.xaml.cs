using KakeiboApp.Pages;

namespace KakeiboApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute("AccountSummaryPage", typeof(AccountSummaryPage));
		Routing.RegisterRoute("EditTransactionPage", typeof(EditTransactionPage));
	}
}
