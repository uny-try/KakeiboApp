namespace KakeiboApp.Core.Models;

public class Account
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? Name { get; set; }
    public decimal Balance { get; set; }

}
