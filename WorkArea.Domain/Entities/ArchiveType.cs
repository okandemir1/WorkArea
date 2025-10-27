using WorkArea.Domain.Entities.Base;

namespace WorkArea.Domain.Entities;

public class ArchiveType :  BaseEntityWithDate
{
    public int UserId { get; set; }
    
    public string Name { get; set; } = string.Empty;
}