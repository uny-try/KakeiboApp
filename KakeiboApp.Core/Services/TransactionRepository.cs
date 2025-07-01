using KakeiboApp.Core.Models;
using KakeiboApp.Core.Interfaces;

namespace KakeiboApp.Core.Services;

public class TransactionRepository : ITransactionRepository
{

    public Task<IEnumerable<Transaction>> GetAllAsync()
    {
        throw new NotImplementedException();
    }
    public Task<Transaction?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }
    public Task AddAsync(Transaction transaction)
    {
        throw new NotImplementedException();
    }
    public Task UpdateAsync(Transaction transaction)
    {
        throw new NotImplementedException();
    }
    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
