namespace SchoolConnect.Common.Domain.Primitives;

public abstract class AggregateRoot : Entity
{
    private readonly List<DomainEvent> _domainEvents = [];
    public IReadOnlyList<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();
    public int Version { get; protected set; }
    
    protected void AddDomainEvent(DomainEvent domainEvent) => _domainEvents.Add(domainEvent);
    public void ClearDomainEvents() => _domainEvents.Clear();
    
    protected void Apply(DomainEvent @event)
    {
        When(@event);
        Version++;
        AddDomainEvent(@event);
    }
    
    protected abstract void When(DomainEvent @event);
}
