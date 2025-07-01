namespace KakeiboApp.Core.Models;

public class Category
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? Name { get; set; }
    public string? Icon { get; set; }
    public CategoryType Type { get; set; }

}
