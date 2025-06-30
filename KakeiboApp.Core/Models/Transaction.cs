namespace KakeiboApp.Core.Models;

public class Transaction
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime Date { get; set; }
    public TransactionType Type { get; set; }
    public Category Category { get; set; }
    public decimal Amount { get; set; }
    public string Note { get; set; }

    // 支出・振替時に使用
    public Account? FromAccount { get; set; }
    // 収入・振替時に使用
    public Account? ToAccount { get; set; }

    public Transaction(DateTime date, TransactionType type, Category category, decimal amount, string note)
    {
        Date = date;
        Type = type;
        Category = category;
        Amount = amount;
        Note = note;
    }
}
