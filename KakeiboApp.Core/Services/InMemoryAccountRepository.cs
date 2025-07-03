using KakeiboApp.Core.Models;
using KakeiboApp.Core.Interfaces;

namespace KakeiboApp.Core.Services;

public class InMemoryAccountRepository : IAccountRepository
{
    private readonly List<Account> _accounts = new List<Account>
    {
        new Account { Name = "現金", Balance = 0 },
        new Account { Name = "楽天Edy", Balance = 0 },
        new Account { Name = "楽天カード", Balance = 0 },
        new Account { Name = "ゆうちょ銀行", Balance = 0 },
        new Account { Name = "楽天銀行", Balance = 0 },
        new Account { Name = "楽天証券", Balance = 0 }
    };

    public Task<IEnumerable<Account>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<Account>>(_accounts);
    }
}
