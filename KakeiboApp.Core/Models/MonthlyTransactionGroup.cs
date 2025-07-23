using System;
using System.Collections.ObjectModel;

namespace KakeiboApp.Core.Models;

public class MonthlyTransactionGroup : ObservableCollection<Transaction>
{
    public string MonthLabel { get; }

    public decimal TotalIncome => CalculateTotalIncome();
    public decimal TotalExpense => CalculateTotalExpense();
    public MonthlyTransactionGroup(string monthLabel, IEnumerable<Transaction> transactions) : base(transactions)
    {
        MonthLabel = monthLabel;
    }

    private decimal CalculateTotalIncome()
    {
        decimal total = 0;
        foreach (var transaction in this)
        {
            if (transaction.Type == TransactionType.Income)
            {
                total += transaction.Amount;
            }
        }
        return total;
    }
    private decimal CalculateTotalExpense()
    {
        decimal total = 0;
        foreach (var transaction in this)
        {
            if (transaction.Type == TransactionType.Expense)
            {
                total += transaction.Amount;
            }
        }
        return total;
    }
}