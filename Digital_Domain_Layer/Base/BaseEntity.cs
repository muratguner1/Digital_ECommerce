namespace Digital_Domain_Layer.Base;

public abstract class BaseEntity
{
    protected BaseEntity()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
    }
    
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
}