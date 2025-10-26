using WorkArea.Domain.Entities.Base;

namespace WorkArea.Domain.Entities;

public class InstallmentItem :  BaseEntityWithDate
{
    public int UserId { get; set; }
    public virtual User User { get; set; }
    
    public int InstallmentId { get; set; }
    public virtual Installment Installment { get; set; }
    
    public decimal Price { get; set; }
    
    public int Count { get; set; }
    public bool HasPayment { get; set; }
    
    public DateTime PaymentDate { get; set; } = DateTime.Now;
}