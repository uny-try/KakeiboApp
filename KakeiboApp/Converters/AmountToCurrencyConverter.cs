using System.Globalization;

namespace KakeiboApp.Converters;

public class AmountToCurrencyConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is decimal amount)
            return amount.ToString("C", culture); // 例："¥1,234"

        return "―"; // 未入力などの場合
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
