using KakeiboApp.ViewModels;

namespace KakeiboApp.Pages;

[QueryProperty(nameof(TransactionId), "TransactionId")]
public partial class EditTransactionPage : ContentPage
{
    public Guid? TransactionId { get; set; }

    private readonly EditTransactionViewModel _viewModel;
    public EditTransactionPage(EditTransactionViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        await _viewModel.LoadSelectionListsAsync();
        if (TransactionId is not null)
        {
            await _viewModel.LoadAsync(TransactionId);
        }
    }

    private void OnCategoryPickerChanged(object sender, EventArgs e)
    {
        if (BindingContext is EditTransactionViewModel vm)
        {
            // 金額がまだ入力されていない場合
            if (vm.Amount == 0)
            {
                // カテゴリ選択時に金額編集を開始
                vm.StartEditingAmountCommand.Execute(null);
            }
        }
    }
}