using System;

using KakeiboApp.Core.Models;
using KakeiboApp.Core.Interfaces;

namespace KakeiboApp.Core.Services;

public class InMemoryTransactionRepository : ITransactionRepository
{
    private readonly List<Transaction> _transactions = new();

    public Task<IEnumerable<Transaction>> GetAllAsync()
    {
        return Task.FromResult(_transactions.AsEnumerable());
    }
    public Task<Transaction?> GetByIdAsync(Guid id)
    {
        return Task.FromResult(_transactions.FirstOrDefault(t => t.Id == id));
    }
    public Task AddAsync(Transaction transaction)
    {
        _transactions.Add(transaction);
        return Task.CompletedTask;
    }
    public Task UpdateAsync(Transaction transaction)
    {
        var index = _transactions.FindIndex(t => t.Id == transaction.Id);
        if (index >= 0)
        {
            _transactions[index] = transaction;
        }
        return Task.CompletedTask;
    }
    public Task DeleteAsync(Guid id)
    {
        var transaction = _transactions.FirstOrDefault(t => t.Id == id);
        if (transaction != null)
        {
            _transactions.Remove(transaction);
        }
        return Task.CompletedTask;
    }
}
