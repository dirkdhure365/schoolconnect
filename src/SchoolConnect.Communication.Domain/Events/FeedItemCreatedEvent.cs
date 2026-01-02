using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.Communication.Domain.Enums;

namespace SchoolConnect.Communication.Domain.Events;

public record FeedItemCreatedEvent(
    Guid FeedItemId,
    Guid UserId,
    FeedItemType Type,
    Guid SourceId) : DomainEvent
{
    public new Guid AggregateId { get; init; } = FeedItemId;
    public new string AggregateType { get; init; } = nameof(Entities.FeedItem);
}
