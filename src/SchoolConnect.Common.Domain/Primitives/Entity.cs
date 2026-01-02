namespace SchoolConnect.Common.Domain.Primitives;

public abstract class Entity
{
    public Guid Id { get; protected set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; protected set; } = DateTime.UtcNow;
    public string? CreatedBy { get; protected set; }
    public string? UpdatedBy { get; protected set; }
    
    public override bool Equals(object? obj) => obj is Entity entity && Id == entity.Id;
    public override int GetHashCode() => Id.GetHashCode();
}
