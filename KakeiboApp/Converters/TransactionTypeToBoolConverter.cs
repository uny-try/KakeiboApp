using System;
using System.Globalization;

using KakeiboApp.Core.Models;

namespace KakeiboApp.Converters;

public class TransactionTypeToBoolConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not TransactionType type || parameter is not string param)
            return false;

        return type.ToString().Equals(param, StringComparison.OrdinalIgnoreCase);
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not bool isChecked || !isChecked || parameter is not string param)
            return Binding.DoNothing;

        return Enum.Parse(typeof(TransactionType), param, true);
    }
}
