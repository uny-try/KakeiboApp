using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using KakeiboApp.Core.Interfaces;
using KakeiboApp.Core.Models;

namespace KakeiboApp.ViewModels;

public partial class AccountSummaryViewModel : ObservableObject
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IAccountRepository _accountRepository;

    [ObservableProperty]
    private ObservableCollection<Account> accounts;

    public AccountSummaryViewModel(ITransactionRepository transactionRepository,
                                   IAccountRepository accountRepository)
    {
        _transactionRepository = transactionRepository;
        _accountRepository = accountRepository;

        Accounts = [];
    }

    public async Task CulculateAccountSummaryAsync()
    {
        var allTransactions = await _transactionRepository.GetAllAsync();
        Accounts = new ObservableCollection<Account>(await _accountRepository.GetAllAsync());

        foreach (var transaction in allTransactions)
        {
            if (transaction.Type == TransactionType.Expense)
            {
                if (transaction.FromAccount is not null)
                {
                    // Find the account associated with the transaction
                    var account = Accounts.FirstOrDefault(a => a.Id == transaction.FromAccount.Id);
                    if (account != null)
                    {
                        // Deduct the expense amount from the account balance
                        account.Balance -= transaction.Amount;
                    }
                }
            }
            else if (transaction.Type == TransactionType.Income)
            {
                if (transaction.ToAccount is not null)
                {
                    // Find the account associated with the transaction
                    var account = Accounts.FirstOrDefault(a => a.Id == transaction.ToAccount.Id);
                    if (account != null)
                    {
                        // Add the income amount to the account balance
                        account.Balance += transaction.Amount;
                    }
                }
            }
            else if (transaction.Type == TransactionType.Transfer)
            {
                // Transfer logic can be more complex, depending on how you want to handle it
                if (transaction.FromAccount is not null && transaction.ToAccount is not null)
                {
                    var fromAccount = Accounts.FirstOrDefault(a => a.Id == transaction.FromAccount.Id);
                    var toAccount = Accounts.FirstOrDefault(a => a.Id == transaction.ToAccount.Id);
                    if (fromAccount != null) fromAccount.Balance -= transaction.Amount;
                    if (toAccount != null) toAccount.Balance += transaction.Amount;
                }
            }
        }
    }
}