using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using KakeiboApp.Core.Models;
using KakeiboApp.Core.Interfaces;

public partial class TransactionViewModel : ObservableObject
{
    private readonly ITransactionRepository _repository;

    [ObservableProperty]
    private ObservableCollection<Transaction> transactions;

    public TransactionViewModel(ITransactionRepository repository)
    {
        _repository = repository;
        transactions = new();
    }

    [RelayCommand]
    private async Task LoadTransactionsAsync()
    {
        var data = await _repository.GetAllAsync();
        Transactions = new ObservableCollection<Transaction>(data);
    }
}
