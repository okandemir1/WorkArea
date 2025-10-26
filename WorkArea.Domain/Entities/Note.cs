using WorkArea.Domain.Entities.Base;

namespace WorkArea.Domain.Entities;

public class Note :  BaseEntityWithDate
{
    public int UserId { get; set; }
    public virtual User User { get; set; }
    
    public string Explanation { get; set; } = string.Empty;
    public DateTime NoteDate { get; set; } = DateTime.Now;
}