using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Communication.Domain.Events;

public record MessageDeletedEvent(
    Guid MessageId,
    Guid ConversationId,
    Guid DeletedBy) : DomainEvent
{
    public new Guid AggregateId { get; init; } = MessageId;
    public new string AggregateType { get; init; } = nameof(Entities.Message);
}
