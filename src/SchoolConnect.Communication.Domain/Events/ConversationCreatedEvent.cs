using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.Communication.Domain.Enums;

namespace SchoolConnect.Communication.Domain.Events;

public record ConversationCreatedEvent(
    Guid ConversationId,
    ConversationType Type,
    List<Guid> ParticipantIds) : DomainEvent
{
    public new Guid AggregateId { get; init; } = ConversationId;
    public new string AggregateType { get; init; } = nameof(Entities.Conversation);
}
