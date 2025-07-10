using System;
using System.Globalization;

using KakeiboApp.Core.Models;

namespace KakeiboApp.Converters;

public class CategoryTypeFilterConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not IEnumerable<Category> categories || parameter is not string typeString)
            return null;

        var type = Enum.TryParse<CategoryType>(typeString, true, out var categoryType) ? categoryType : CategoryType.Expense;

        return categories.Where(c => c.Type == type).ToList();
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
