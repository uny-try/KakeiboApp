using KakeiboApp.Core.Models;
using KakeiboApp.Core.Interfaces;

namespace KakeiboApp.Core.Services;

public class InMemoryCategoryRepository : ICategoryRepository
{
    public Task<IEnumerable<Category>> GetAllAsync()
    {
        // Simulate fetching categories from an in-memory store
        var categories = new List<Category>
        {
            new Category { Name = "食費", Icon = "\uE56C", Type = CategoryType.Expense },
            new Category { Name = "交通費", Icon = "\uE530", Type = CategoryType.Expense },
            new Category { Name = "光熱費", Icon = "\uE0F0", Type = CategoryType.Expense },
            new Category { Name = "通信費", Icon = "\uE0B0", Type = CategoryType.Expense },
            new Category { Name = "その他", Icon = "\uF526", Type = CategoryType.Expense },
            new Category { Name = "給与", Icon = "\uE8D1", Type = CategoryType.Income },
            new Category { Name = "その他", Icon = "\uF526", Type = CategoryType.Income }

        };

        return Task.FromResult<IEnumerable<Category>>(categories);
    }
}
