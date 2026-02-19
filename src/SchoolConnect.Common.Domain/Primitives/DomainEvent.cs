using MediatR;

namespace SchoolConnect.Common.Domain.Primitives;

public abstract record DomainEvent : INotification
{
    public Guid EventId { get; init; } = Guid.NewGuid();
    public DateTime OccurredOn { get; init; } = DateTime.UtcNow;
    public Guid AggregateId { get; init; }
    public string AggregateType { get; init; } = string.Empty;
    public int Version { get; init; }
}
