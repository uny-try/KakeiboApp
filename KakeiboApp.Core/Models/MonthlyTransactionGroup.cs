using System;
using System.Collections.ObjectModel;

namespace KakeiboApp.Core.Models;

public class MonthlyTransactionGroup : ObservableCollection<Transaction>
{
    public string MonthLabel { get; }

    public MonthlyTransactionGroup(string monthLabel, IEnumerable<Transaction> transactions) : base(transactions)
    {
        MonthLabel = monthLabel;
    }
}