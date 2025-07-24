using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using KakeiboApp.Core.Models;
using KakeiboApp.Core.Interfaces;

namespace KakeiboApp.ViewModels;

public partial class TransactionViewModel : ObservableObject
{
    private readonly ITransactionRepository _repository;
    private readonly INavigationService _navigationService;

    [ObservableProperty]
    private DateTime _currentMonth = DateTime.Now;

    [ObservableProperty]
    private ObservableCollection<Transaction> transactions;

    [ObservableProperty]
    private ObservableCollection<MonthlyTransactionGroup> monthlyTransactionGroups = new();

    [ObservableProperty]
    private Transaction? selectedTransaction;

    public TransactionViewModel(ITransactionRepository repository, INavigationService navigationService)
    {
        _repository = repository;
        _navigationService = navigationService;
        transactions = new();
        selectedTransaction = null;
    }

    [RelayCommand]
    private async Task LoadTransactionsAsync()
    {
        var data = (await _repository.GetAllAsync()).OrderBy(t => t.Date);
        Transactions = new ObservableCollection<Transaction>(data);
        GroupTransactionsByMonth(Transactions);
    }

    [RelayCommand]
    public async Task GoToEditAsync(Guid? transactionId = null)
    {
        var route = "EditTransactionPage";
        if (transactionId.HasValue)
        {
            route += $"?TransactionId={transactionId.Value}";
        }
        await _navigationService.NavigateToAsync(route);
        SelectedTransaction = null; // Reset after navigation
    }

    [RelayCommand]
    public async Task GoToAccountSummaryAsync()
    {
        await _navigationService.NavigateToAsync("AccountSummaryPage");
    }

    [RelayCommand]
    public async Task DeleteTransactionAsync(Guid transactionId)
    {
        if (transactionId == Guid.Empty)
            return;

        await _repository.DeleteAsync(transactionId);
        await LoadTransactionsAsync(); // Reload after deletion
        SelectedTransaction = null; // Reset selected transaction
    }

    [RelayCommand]
    public async Task NextMonthAsync()
    {
        CurrentMonth = CurrentMonth.AddMonths(1);
        await LoadTransactionsAsync();
    }

    [RelayCommand]
    public async Task PreviousMonthAsync()
    {
        CurrentMonth = CurrentMonth.AddMonths(-1);
        await LoadTransactionsAsync();
    }

    [RelayCommand]
    private void GroupTransactionsByMonth(IEnumerable<Transaction> transactions)
    {
        MonthlyTransactionGroups.Clear();
        var grouped = transactions
            .GroupBy(t => new DateTime(t.Date.Year, t.Date.Month, 1))
            .OrderBy(g => g.Key);

        foreach (var group in grouped)
        {
            var monthLabel = group.Key.ToString("yyyy年MM月");
            var monthlyGroup = new MonthlyTransactionGroup(monthLabel, group);
            MonthlyTransactionGroups.Add(monthlyGroup);
        }
    }
}
