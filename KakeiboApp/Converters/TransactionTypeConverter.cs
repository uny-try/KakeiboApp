using System.Globalization;

using KakeiboApp.Core.Models;

namespace KakeiboApp.Converters;

public class TransactionTypeConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is TransactionType transactionType)
        {
            return transactionType switch
            {
                TransactionType.Expense => "支出",
                TransactionType.Income => "収入",
                TransactionType.Transfer => "振替",
                _ => "不明"
            };
        }
        return "不明";
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
