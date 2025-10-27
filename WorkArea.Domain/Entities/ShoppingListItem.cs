using WorkArea.Domain.Entities.Base;

namespace WorkArea.Domain.Entities;

public class ShoppingListItem :  BaseEntityWithDate
{
    public int ShoppingListId { get; set; }
    public virtual ShoppingList ShoppingList { get; set; }
    
    public int UserId { get; set; }
    
    public string Name { get; set; } = string.Empty;
}