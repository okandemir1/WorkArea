using WorkArea.Domain.Entities.Base;

namespace WorkArea.Domain.Entities;

public class Archive :  BaseEntityWithDate
{
    public int UserId { get; set; }
    public virtual User User { get; set; }
    
    public int ArchiveTypeId { get; set; }
    public virtual ArchiveType ArchiveType { get; set; }
    
    public string Title { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public DateTime? BirthDate { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}