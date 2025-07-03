using KakeiboApp.Core.Models;

namespace KakeiboApp.Core.Interfaces;

public interface IAccountRepository
{
    Task<IEnumerable<Account>> GetAllAsync();

}
