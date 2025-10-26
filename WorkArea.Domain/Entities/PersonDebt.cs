using WorkArea.Domain.Entities.Base;

namespace WorkArea.Domain.Entities;

public class PersonDebt :  BaseEntityWithDate
{
    public int UserId { get; set; }
    public virtual User User { get; set; }
    
    public int PersonId { get; set; }
    public virtual Person Person { get; set; }
    
    public decimal TotalPrice { get; set; }
}