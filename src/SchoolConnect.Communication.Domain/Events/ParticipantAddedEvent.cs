using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Communication.Domain.Events;

public record ParticipantAddedEvent(
    Guid ConversationId,
    Guid ParticipantId,
    Guid AddedBy) : DomainEvent
{
    public new Guid AggregateId { get; init; } = ConversationId;
    public new string AggregateType { get; init; } = nameof(Entities.Conversation);
}
