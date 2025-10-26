using WorkArea.Domain.Entities.Base;

namespace WorkArea.Domain.Entities;

public class Expense :  BaseEntityWithDate
{
    public int UserId { get; set; }
    public virtual User User { get; set; }
    
    public decimal TotalPrice { get; set; }
    public string FilePath { get; set; } = string.Empty;
    public string ImagePath { get; set; } = string.Empty;
    public DateTime PaymentDate { get; set; } = DateTime.Now;
}