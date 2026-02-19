using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Common.Infrastructure.Events;

public interface IDomainEventDispatcher
{
    Task DispatchEventsAsync(IEnumerable<DomainEvent> events, CancellationToken ct = default);
}
