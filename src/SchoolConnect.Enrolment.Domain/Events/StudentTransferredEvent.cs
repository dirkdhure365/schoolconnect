using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Enrolment.Domain.Events;

public record StudentTransferredEvent : DomainEvent
{
    public Guid StudentId { get; init; }
    public Guid FromStreamId { get; init; }
    public Guid ToStreamId { get; init; }
    public DateTime TransferredAt { get; init; }
}
