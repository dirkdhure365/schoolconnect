using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Communication.Domain.Events;

public record MessageReadEvent(
    Guid MessageId,
    Guid ConversationId,
    Guid UserId) : DomainEvent
{
    public new Guid AggregateId { get; init; } = MessageId;
    public new string AggregateType { get; init; } = nameof(Entities.Message);
}
