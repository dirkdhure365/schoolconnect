using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Common.Infrastructure.EventDispatcher;

public interface IDomainEventDispatcher
{
    Task DispatchAsync(IEnumerable<DomainEvent> events, CancellationToken ct = default);
    Task DispatchAsync(DomainEvent @event, CancellationToken ct = default);
}
