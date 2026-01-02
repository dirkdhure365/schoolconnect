using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.Subscription.Domain.Enums;
using SchoolConnect.Subscription.Domain.Events;

namespace SchoolConnect.Subscription.Domain.Entities;

public class Subscription : AggregateRoot
{
    public Guid InstituteId { get; private set; }
    public Guid PlanId { get; private set; }
    public SubscriptionStatus Status { get; private set; }
    public BillingFrequency BillingFrequency { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime? EndDate { get; private set; }
    public DateTime? NextBillingDate { get; private set; }
    public DateTime? TrialStartDate { get; private set; }
    public DateTime? TrialEndDate { get; private set; }
    public bool AutoRenew { get; private set; }
    public DateTime? CancelledAt { get; private set; }
    public string? CancellationReason { get; private set; }

    private Subscription() { }

    public static Subscription Create(
        Guid instituteId,
        Guid planId,
        BillingFrequency billingFrequency,
        DateTime startDate,
        bool autoRenew = true)
    {
        var subscription = new Subscription
        {
            InstituteId = instituteId,
            PlanId = planId,
            BillingFrequency = billingFrequency,
            StartDate = startDate,
            Status = SubscriptionStatus.Active,
            AutoRenew = autoRenew,
            NextBillingDate = CalculateNextBillingDate(startDate, billingFrequency)
        };

        subscription.AddDomainEvent(new SubscriptionCreatedEvent
        {
            AggregateId = subscription.Id,
            EventType = nameof(SubscriptionCreatedEvent),
            InstituteId = instituteId,
            PlanId = planId
        });

        return subscription;
    }

    public void Activate()
    {
        Status = SubscriptionStatus.Active;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new SubscriptionActivatedEvent
        {
            AggregateId = Id,
            EventType = nameof(SubscriptionActivatedEvent),
            InstituteId = InstituteId
        });
    }

    public void Upgrade(Guid newPlanId)
    {
        var oldPlanId = PlanId;
        PlanId = newPlanId;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new SubscriptionUpgradedEvent
        {
            AggregateId = Id,
            EventType = nameof(SubscriptionUpgradedEvent),
            InstituteId = InstituteId,
            OldPlanId = oldPlanId,
            NewPlanId = newPlanId
        });
    }

    public void Downgrade(Guid newPlanId)
    {
        var oldPlanId = PlanId;
        PlanId = newPlanId;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new SubscriptionDowngradedEvent
        {
            AggregateId = Id,
            EventType = nameof(SubscriptionDowngradedEvent),
            InstituteId = InstituteId,
            OldPlanId = oldPlanId,
            NewPlanId = newPlanId
        });
    }

    public void Cancel(string? reason = null)
    {
        Status = SubscriptionStatus.Cancelled;
        CancelledAt = DateTime.UtcNow;
        CancellationReason = reason;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new SubscriptionCancelledEvent
        {
            AggregateId = Id,
            EventType = nameof(SubscriptionCancelledEvent),
            InstituteId = InstituteId,
            Reason = reason
        });
    }

    public void Suspend()
    {
        Status = SubscriptionStatus.Suspended;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Expire()
    {
        Status = SubscriptionStatus.Expired;
        EndDate = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new SubscriptionExpiredEvent
        {
            AggregateId = Id,
            EventType = nameof(SubscriptionExpiredEvent),
            InstituteId = InstituteId
        });
    }

    public void Renew()
    {
        Status = SubscriptionStatus.Active;
        NextBillingDate = CalculateNextBillingDate(DateTime.UtcNow, BillingFrequency);
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new SubscriptionRenewedEvent
        {
            AggregateId = Id,
            EventType = nameof(SubscriptionRenewedEvent),
            InstituteId = InstituteId,
            NextBillingDate = NextBillingDate.Value
        });
    }

    public void StartTrial(DateTime trialStartDate, DateTime trialEndDate)
    {
        Status = SubscriptionStatus.Trial;
        TrialStartDate = trialStartDate;
        TrialEndDate = trialEndDate;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new TrialStartedEvent
        {
            AggregateId = Id,
            EventType = nameof(TrialStartedEvent),
            InstituteId = InstituteId,
            TrialEndDate = trialEndDate
        });
    }

    private static DateTime CalculateNextBillingDate(DateTime startDate, BillingFrequency frequency)
    {
        return frequency switch
        {
            BillingFrequency.Monthly => startDate.AddMonths(1),
            BillingFrequency.Quarterly => startDate.AddMonths(3),
            BillingFrequency.Annual => startDate.AddYears(1),
            _ => startDate.AddMonths(1)
        };
    }

    protected override void When(DomainEvent @event)
    {
        // Event sourcing pattern - not implemented yet
    }
}
