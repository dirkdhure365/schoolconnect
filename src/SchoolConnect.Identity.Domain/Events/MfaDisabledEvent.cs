using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Identity.Domain.Events;

public record MfaDisabledEvent : DomainEvent
{
    public MfaDisabledEvent(Guid aggregateId, int version)
    {
        AggregateId = aggregateId;
        AggregateType = "User";
        Version = version;
    }
}
