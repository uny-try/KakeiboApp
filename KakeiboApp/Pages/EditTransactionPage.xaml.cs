using KakeiboApp.ViewModels;

namespace KakeiboApp.Pages;

[QueryProperty(nameof(TransactionIdString), "TransactionId")]
public partial class EditTransactionPage : ContentPage
{
    public string? TransactionIdString { get; set; }
    public Guid? TransactionId;

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
        if (TransactionIdString is not null)
        {
            if (Guid.TryParse(TransactionIdString, out var id))
            {
                TransactionId = id;
                await _viewModel.LoadAsync(TransactionId);
            }
            else
            {
                // 文字列がGuidに変換できない場合のエラーハンドリング
                await DisplayAlert("Error", "Invalid Transaction ID format.", "OK");
                return;
            }
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