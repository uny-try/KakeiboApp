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
    [ObservableProperty] private TransactionType type;
    [ObservableProperty] private DateTime date = DateTime.Now;
    [ObservableProperty] private Account? fromAccount;
    [ObservableProperty] private Account? toAccount;
    [ObservableProperty] private Category? selectedCategory;
    [ObservableProperty] private decimal amount;
    [ObservableProperty] private string? note;

    // 選択肢のリスト
    [ObservableProperty] private List<Category> categories;
    [ObservableProperty] private List<Account> accounts;

    // 表示切替用
    public bool IsExpense => Type == TransactionType.Expense;
    public bool IsIncome => Type == TransactionType.Income;
    public bool IsTransfer => Type == TransactionType.Transfer;

    // 金額入力用
    [ObservableProperty] private bool isEditingAmount = false;

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

        Type = TransactionType.Expense; // デフォルトは支出
    }

    public async Task LoadAsync(Guid? id = null)
    {
        if (id is null)
            return;

        var tx = await _repository.GetByIdAsync(id.Value);
        if (tx is null)
            return;

        TransactionId = tx.Id;
        Type = tx.Type;
        Date = tx.Date;
        FromAccount = tx.FromAccount;
        ToAccount = tx.ToAccount;
        SelectedCategory = tx.Category;
        Amount = tx.Amount;
        Note = tx.Note;
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
            Type = Type,
            Date = Date,
            FromAccount = FromAccount,
            ToAccount = ToAccount,
            Category = SelectedCategory,
            Amount = Amount,
            Note = Note
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

    partial void OnTypeChanged(TransactionType value)
    {
        OnPropertyChanged(nameof(IsExpense));
        OnPropertyChanged(nameof(IsIncome));
        OnPropertyChanged(nameof(IsTransfer));
    }

    [RelayCommand]
    public void StartEditingAmount()
    {
        IsEditingAmount = true;
    }

    [RelayCommand]
    public void FinishEditingAmount()
    {
        IsEditingAmount = false;
    }
}
