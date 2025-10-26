using WorkArea.Domain.Entities.Base;

namespace WorkArea.Domain.Entities;

public class NoteMention :  BaseEntityWithDate
{
    public int UserId { get; set; }
    public virtual User User { get; set; }
    
    public int NoteId { get; set; }
    public virtual Note Note { get; set; }
    
    public string Name { get; set; } = string.Empty;
}