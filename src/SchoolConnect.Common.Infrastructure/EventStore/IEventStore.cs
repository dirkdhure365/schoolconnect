using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Common.Infrastructure.EventStore;

public interface IEventStore
{
    Task SaveEventsAsync(Guid aggregateId, IEnumerable<DomainEvent> events, int expectedVersion, CancellationToken ct = default);
    Task<IEnumerable<DomainEvent>> GetEventsAsync(Guid aggregateId, CancellationToken ct = default);
    Task<IEnumerable<DomainEvent>> GetEventsAfterAsync(Guid aggregateId, int version, CancellationToken ct = default);
}
