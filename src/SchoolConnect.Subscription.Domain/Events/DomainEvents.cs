using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.Subscription.Domain.Enums;

namespace SchoolConnect.Subscription.Domain.Events;

public record PlanCreatedEvent : DomainEvent
{
    public required string EventType { get; init; }
    public required string Name { get; init; }
    public required string Code { get; init; }
    public required PlanTier Tier { get; init; }
}

public record PlanDeactivatedEvent : DomainEvent
{
    public required string EventType { get; init; }
}

public record SubscriptionCreatedEvent : DomainEvent
{
    public required string EventType { get; init; }
    public required Guid InstituteId { get; init; }
    public required Guid PlanId { get; init; }
}

public record SubscriptionActivatedEvent : DomainEvent
{
    public required string EventType { get; init; }
    public required Guid InstituteId { get; init; }
}

public record SubscriptionUpgradedEvent : DomainEvent
{
    public required string EventType { get; init; }
    public required Guid InstituteId { get; init; }
    public required Guid OldPlanId { get; init; }
    public required Guid NewPlanId { get; init; }
}

public record SubscriptionDowngradedEvent : DomainEvent
{
    public required string EventType { get; init; }
    public required Guid InstituteId { get; init; }
    public required Guid OldPlanId { get; init; }
    public required Guid NewPlanId { get; init; }
}

public record SubscriptionCancelledEvent : DomainEvent
{
    public required string EventType { get; init; }
    public required Guid InstituteId { get; init; }
    public string? Reason { get; init; }
}

public record SubscriptionExpiredEvent : DomainEvent
{
    public required string EventType { get; init; }
    public required Guid InstituteId { get; init; }
}

public record SubscriptionRenewedEvent : DomainEvent
{
    public required string EventType { get; init; }
    public required Guid InstituteId { get; init; }
    public required DateTime NextBillingDate { get; init; }
}

public record TrialStartedEvent : DomainEvent
{
    public required string EventType { get; init; }
    public required Guid InstituteId { get; init; }
    public required DateTime TrialEndDate { get; init; }
}

public record TrialConvertedEvent : DomainEvent
{
    public required string EventType { get; init; }
    public required Guid InstituteId { get; init; }
    public required Guid SubscriptionId { get; init; }
}

public record TrialExpiredEvent : DomainEvent
{
    public required string EventType { get; init; }
    public required Guid InstituteId { get; init; }
}

public record UsageLimitApproachingEvent : DomainEvent
{
    public required string EventType { get; init; }
    public required Guid InstituteId { get; init; }
    public required Guid SubscriptionId { get; init; }
    public required FeatureType FeatureType { get; init; }
    public required int CurrentUsage { get; init; }
    public required int Limit { get; init; }
}

public record UsageLimitExceededEvent : DomainEvent
{
    public required string EventType { get; init; }
    public required Guid InstituteId { get; init; }
    public required Guid SubscriptionId { get; init; }
    public required FeatureType FeatureType { get; init; }
    public required int CurrentUsage { get; init; }
    public required int Limit { get; init; }
}
