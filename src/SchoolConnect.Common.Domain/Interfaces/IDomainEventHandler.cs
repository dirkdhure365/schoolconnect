using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Common.Domain.Interfaces;

public interface IDomainEventHandler<in TDomainEvent> where TDomainEvent : DomainEvent
{
    Task Handle(TDomainEvent domainEvent, CancellationToken ct = default);
}
