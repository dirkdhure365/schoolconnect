using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Communication.Domain.Events;

public record MessageSentEvent(
    Guid MessageId,
    Guid ConversationId,
    Guid SenderId,
    string Content) : DomainEvent
{
    public new Guid AggregateId { get; init; } = MessageId;
    public new string AggregateType { get; init; } = nameof(Entities.Message);
}
