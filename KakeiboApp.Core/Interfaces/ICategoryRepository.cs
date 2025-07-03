using KakeiboApp.Core.Models;

namespace KakeiboApp.Core.Interfaces;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAllAsync();
}
