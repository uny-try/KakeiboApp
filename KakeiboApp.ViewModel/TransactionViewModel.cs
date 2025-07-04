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
    private ObservableCollection<Transaction> transactions;

    public TransactionViewModel(ITransactionRepository repository, INavigationService navigationService)
    {
        _repository = repository;
        _navigationService = navigationService;
        transactions = new();
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
        var parameters = new Dictionary<string, object>();
        if (transactionId.HasValue)
        {
            parameters.Add("TransactionId", transactionId.Value);
        }
        await _navigationService.NavigateToAsync("EditTransactionPage", parameters);
    }
}
