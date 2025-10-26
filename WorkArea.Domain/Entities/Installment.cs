using WorkArea.Domain.Entities.Base;

namespace WorkArea.Domain.Entities;

public class Installment :  BaseEntityWithDate
{
    public int UserId { get; set; }
    public virtual User User { get; set; }
    
    public decimal TotalPrice { get; set; }
    
    public string Title { get; set; } = string.Empty;
    public string Explanation { get; set; } = string.Empty;
    
    public DateTime StartDate { get; set; } = DateTime.Now;
    public DateTime FinishDate { get; set; } = DateTime.Now;
    public DateTime PaymentDate { get; set; } = DateTime.Now;
}