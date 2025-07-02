using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using KakeiboApp.Core.Models;
using KakeiboApp.Core.Interfaces;

public partial class EditTransactionViewModel : ObservableObject
{
    private readonly ITransactionRepository _repository;
    private readonly INavigationService _navigation;

    [ObservableProperty] private Guid? transactionId;
    [ObservableProperty] private decimal amount;
    [ObservableProperty] private string? note;
    [ObservableProperty] private DateTime date = DateTime.Today;
    [ObservableProperty] private TransactionType type;
    [ObservableProperty] private Category? selectedCategory;
    [ObservableProperty] private Account? fromAccount;
    [ObservableProperty] private Account? toAccount;

    public EditTransactionViewModel(
        ITransactionRepository repository,
        INavigationService navigation)
    {
        _repository = repository;
        _navigation = navigation;
    }

    public async Task LoadAsync(Guid? id = null)
    {
        if (id is null)
            return;

        var tx = await _repository.GetByIdAsync(id.Value);
        if (tx is null)
            return;

        TransactionId = tx.Id;
        Amount = tx.Amount;
        Note = tx.Note;
        Date = tx.Date;
        Type = tx.Type;
        SelectedCategory = tx.Category;
        FromAccount = tx.FromAccount;
        ToAccount = tx.ToAccount;
    }

    [RelayCommand]
    public async Task SaveAsync()
    {
        var model = new Transaction
        {
            Id = TransactionId ?? Guid.NewGuid(),
            Amount = Amount,
            Note = Note,
            Date = Date,
            Type = Type,
            Category = SelectedCategory,
            FromAccount = FromAccount,
            ToAccount = ToAccount
        };

        if (TransactionId is null)
            await _repository.AddAsync(model);
        else
            await _repository.UpdateAsync(model);

        await _navigation.GoBackAsync();
    }
}
