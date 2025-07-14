using System;
using System.Text.Json;

using KakeiboApp.Core.Interfaces;
using KakeiboApp.Core.Models;

namespace KakeiboApp.Core.Services;

public class FileCategoryRepository : ICategoryRepository
{
    private readonly string _filePath;

    public FileCategoryRepository(string folderPath)
    {
        _filePath = Path.Combine(folderPath, "categories.json");
    }

    public Task<IEnumerable<Category>> GetAllAsync()
    {
        var json = string.Empty;
        var categories = new List<Category>();

        if (!File.Exists(_filePath))
        {
            // ファイルが存在しない場合
            categories =
            [
                new Category { Name = "食費", Icon = "\uE56C", Type = CategoryType.Expense },
                new Category { Name = "交通費", Icon = "\uE530", Type = CategoryType.Expense },
                new Category { Name = "通信費", Icon = "\uE0B0", Type = CategoryType.Expense },
                new Category { Name = "衣服", Icon = "\uE0F0", Type = CategoryType.Expense },
                new Category { Name = "生活用品", Icon = "\uE8D2", Type = CategoryType.Expense },
                new Category { Name = "娯楽", Icon = "\uE8D0", Type = CategoryType.Expense },
                new Category { Name = "その他", Icon = "\uF526", Type = CategoryType.Expense },
                new Category { Name = "給与", Icon = "\uE8D1", Type = CategoryType.Income },
                new Category { Name = "その他", Icon = "\uF526", Type = CategoryType.Income }
            ];

            // 初期データをファイルに保存
            json = JsonSerializer.Serialize(categories);
            File.WriteAllText(_filePath, json);
            return Task.FromResult<IEnumerable<Category>>(categories);
        }

        json = File.ReadAllText(_filePath);
        categories = JsonSerializer.Deserialize<List<Category>>(json) ?? [];
        return Task.FromResult<IEnumerable<Category>>(categories);
    }
}