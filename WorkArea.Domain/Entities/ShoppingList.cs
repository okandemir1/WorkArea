using WorkArea.Domain.Entities.Base;

namespace WorkArea.Domain.Entities;

public class ShoppingList :  BaseEntityWithDate
{
    public int UserId { get; set; }
    public virtual User User { get; set; }
    
    public string Title { get; set; } = string.Empty;
    public string ShortCode { get; set; } = string.Empty;
}