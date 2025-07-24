using KakeiboApp.ViewModels;

namespace KakeiboApp.Pages;

public partial class AccountSummaryPage : ContentPage
{
	private readonly AccountSummaryViewModel _viewModel;
	public AccountSummaryPage(AccountSummaryViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
		BindingContext = _viewModel;
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		var viewModel = BindingContext as AccountSummaryViewModel;
		if (viewModel != null)
		{
			await viewModel.CulculateAccountSummaryAsync();
		}
	}
}