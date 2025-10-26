using WorkArea.Domain.Entities.Base;

namespace WorkArea.Domain.Entities;

public class PersonDebtHistory :  BaseEntityWithDate
{
    public int UserId { get; set; }
    public virtual User User { get; set; }
    
    public int PersonId { get; set; }
    public virtual Person Person { get; set; }
    
    public int PersonDebtId { get; set; }
    public virtual PersonDebt PersonDebt { get; set; }
    
    public decimal PaymentPrice { get; set; }
    public DateTime PaymentDate { get; set; } = DateTime.Now;
}