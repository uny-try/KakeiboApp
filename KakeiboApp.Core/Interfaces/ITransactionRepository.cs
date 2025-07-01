using System;
using KakeiboApp.Core.Models;

namespace KakeiboApp.Core.Interfaces;

public interface ITransactionRepository
{
    Task<IEnumerable<Transaction>> GetAllAsync();
    Task<Transaction?> GetByIdAsync(Guid id);
    Task AddAsync(Transaction transaction);
    Task UpdateAsync(Transaction transaction);
    Task DeleteAsync(Guid id);
}
