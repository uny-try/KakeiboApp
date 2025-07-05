using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using KakeiboApp.Core.Models;
using KakeiboApp.Core.Interfaces;
using System.Collections.ObjectModel;

namespace KakeiboApp.ViewModels;

public partial class EditTransactionViewModel : ObservableObject
{
    private readonly ITransactionRepository _repository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly INavigationService _navigation;

    [ObservableProperty] private Guid? transactionId = null;
    [ObservableProperty] private decimal amount;
    [ObservableProperty] private string? note;
    [ObservableProperty] private DateTime date = DateTime.Now;
    [ObservableProperty] private TransactionType type;
    [ObservableProperty] private Category? selectedCategory;
    [ObservableProperty] private Account? fromAccount;
    [ObservableProperty] private Account? toAccount;

    [ObservableProperty] private List<Category> categories;
    [ObservableProperty] private List<Account> accounts;

    public ObservableCollection<TransactionType> TransactionTypes { get; } =
        new ObservableCollection<TransactionType>((TransactionType[])Enum.GetValues(typeof(TransactionType)));

    public EditTransactionViewModel(
        ITransactionRepository repository,
        ICategoryRepository categoryRepository,
        IAccountRepository accountRepository,
        INavigationService navigation)
    {
        _repository = repository;
        _categoryRepository = categoryRepository;
        _accountRepository = accountRepository;
        _navigation = navigation;

        Categories = new();
        Accounts = new();
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

    public async Task LoadSelectionListsAsync()
    {
        Categories = (await _categoryRepository.GetAllAsync()).ToList();
        Accounts = (await _accountRepository.GetAllAsync()).ToList();
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
    
    [RelayCommand]
    public async Task CancelAsync()
    {
        await _navigation.GoBackAsync();
    }
}
