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
		if (TransactionId is not null)
		{
			await _viewModel.LoadAsync(TransactionId);
		}
	}
}