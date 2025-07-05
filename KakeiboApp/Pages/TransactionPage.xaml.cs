using KakeiboApp.ViewModels;

namespace KakeiboApp.Pages;

public partial class TransactionPage : ContentPage
{
    private readonly TransactionViewModel _viewModel;
    public TransactionPage(TransactionViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.LoadTransactionsCommand.Execute(null);
    }
}