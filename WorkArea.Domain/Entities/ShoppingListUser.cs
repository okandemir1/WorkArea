using WorkArea.Domain.Entities.Base;

namespace WorkArea.Domain.Entities;

public class ShoppingListUser :  BaseEntity
{
    public int ShoppingListId { get; set; }
    public int UserId { get; set; }
}