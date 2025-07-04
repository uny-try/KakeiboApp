using KakeiboApp.Pages;

namespace KakeiboApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute("EditTransactionPage", typeof(EditTransactionPage));
	}
}
