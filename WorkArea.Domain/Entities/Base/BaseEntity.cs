using System.ComponentModel.DataAnnotations;

namespace WorkArea.Domain.Entities.Base;

public abstract class BaseEntity
{
    [Key]
    public int Id { get; set; }
    
    public bool IsDeleted { get; set; }
}

public abstract class BaseEntityWithDate : BaseEntity
{
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }
}