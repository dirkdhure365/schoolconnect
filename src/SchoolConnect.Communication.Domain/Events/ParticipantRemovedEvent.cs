using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Communication.Domain.Events;

public record ParticipantRemovedEvent(
    Guid ConversationId,
    Guid ParticipantId,
    Guid RemovedBy) : DomainEvent
{
    public new Guid AggregateId { get; init; } = ConversationId;
    public new string AggregateType { get; init; } = nameof(Entities.Conversation);
}
