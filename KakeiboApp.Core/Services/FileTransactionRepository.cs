using System.Text.Json;

using KakeiboApp.Core.Interfaces;
using KakeiboApp.Core.Models;

namespace KakeiboApp.Core.Services;

public class FileTransactionRepository : ITransactionRepository
{
    private readonly string _filePath;

    public FileTransactionRepository(string folderPath)
    {
        _filePath = Path.Combine(folderPath, "transactions.json");
        Console.WriteLine($"File path for transactions: {Path.GetFullPath(_filePath)}");
    }

    public Task AddAsync(Transaction transaction)
    {
        return GetAllAsync().ContinueWith(task =>
        {
            var transactions = task.Result.ToList();
            transactions.Add(transaction);
            var json = JsonSerializer.Serialize(transactions, new JsonSerializerOptions { WriteIndented = true });
            return File.WriteAllTextAsync(_filePath, json);
        });
    }

    public Task DeleteAsync(Guid id)
    {
        return GetAllAsync().ContinueWith(task =>
        {
            var transactions = task.Result.ToList();
            var transactionToRemove = transactions.FirstOrDefault(t => t.Id == id);
            if (transactionToRemove != null)
            {
                transactions.Remove(transactionToRemove);
                var json = JsonSerializer.Serialize(transactions, new JsonSerializerOptions { WriteIndented = true });
                return File.WriteAllTextAsync(_filePath, json);
            }
            return Task.CompletedTask;
        });
    }

    public async Task<IEnumerable<Transaction>> GetAllAsync()
    {
        if (!File.Exists(_filePath))
        {
            return [];
        }

        var json = await File.ReadAllTextAsync(_filePath);
        return JsonSerializer.Deserialize<IEnumerable<Transaction>>(json)
               ?? [];
    }

    public Task<Transaction?> GetByIdAsync(Guid id)
    {
        return GetAllAsync().ContinueWith(task =>
        {
            return task.Result.FirstOrDefault(t => t.Id == id);
        });
    }

    public Task UpdateAsync(Transaction transaction)
    {
        return GetAllAsync().ContinueWith(task =>
        {
            var transactions = task.Result.ToList();
            var index = transactions.FindIndex(t => t.Id == transaction.Id);
            if (index != -1)
            {
                transactions[index] = transaction;
                var json = JsonSerializer.Serialize(transactions, new JsonSerializerOptions { WriteIndented = true });
                return File.WriteAllTextAsync(_filePath, json);
            }
            return Task.CompletedTask;
        });
    }
}
