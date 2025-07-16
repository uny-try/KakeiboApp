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
        var data = await _repository.GetAllAsync();
        Transactions = new ObservableCollection<Transaction>(data);
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
}
