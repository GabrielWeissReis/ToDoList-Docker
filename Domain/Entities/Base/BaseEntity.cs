namespace Domain.Entities.Base;

public class BaseEntity
{
    public int Id { get; set; }

    public bool Active { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? EditedAt { get; set; }

    public DateTime? DeletedAt { get; set; }
}
