using System;
using System.Text.Json;

using KakeiboApp.Core.Interfaces;
using KakeiboApp.Core.Models;

namespace KakeiboApp.Core.Services;

public class FileAccountRepository : IAccountRepository
{
    private readonly string _filePath;

    public FileAccountRepository(string folderPath)
    {
        _filePath = Path.Combine(folderPath, "accounts.json");
    }

    public Task<IEnumerable<Account>> GetAllAsync()
    {
        var json = string.Empty;
        var accounts = new List<Account>();

        if (!File.Exists(_filePath))
        {
            // ファイルが存在しない場合
            accounts =
            [
                new Account { Name = "現金", Balance = 0 },
                new Account { Name = "楽天Edy", Balance = 0 },
                new Account { Name = "楽天カード", Balance = 0 },
                new Account { Name = "秋田銀行", Balance = 0 },
                new Account { Name = "ゆうちょ銀行", Balance = 0 },
                new Account { Name = "楽天証券", Balance = 0 }
            ];

            // 初期データをファイルに保存
            json = JsonSerializer.Serialize(accounts);
            File.WriteAllText(_filePath, json);
            return Task.FromResult<IEnumerable<Account>>(accounts);
        }

        json = File.ReadAllText(_filePath);
        accounts = JsonSerializer.Deserialize<List<Account>>(json) ?? [];
        return Task.FromResult<IEnumerable<Account>>(accounts);
    }
}