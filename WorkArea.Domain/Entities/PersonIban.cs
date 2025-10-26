using WorkArea.Domain.Entities.Base;

namespace WorkArea.Domain.Entities;

public class PersonIban :  BaseEntityWithDate
{
    public int UserId { get; set; }
    public virtual User User { get; set; }
    
    public int PersonId { get; set; }
    public virtual Person Person { get; set; }
    
    public string BankName { get; set; } = string.Empty;
    public string Iban { get; set; } = string.Empty;
    public string Fullname { get; set; } = string.Empty;
}