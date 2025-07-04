using KakeiboApp.ViewModels;

namespace KakeiboApp.Pages;

public partial class TransactionPage : ContentPage
{
	private readonly TransactionViewModel _viewModel;
	public TransactionPage(TransactionViewModel viewModel)
	{
		_viewModel = viewModel;
		BindingContext = _viewModel;
		InitializeComponent();
	}
}