using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.Subscription.Domain.Enums;
using SchoolConnect.Subscription.Domain.Events;

namespace SchoolConnect.Subscription.Domain.Entities;

public class SubscriptionTrial : AggregateRoot
{
    public Guid SubscriptionId { get; private set; }
    public Guid InstituteId { get; private set; }
    public Guid PlanId { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public TrialStatus Status { get; private set; }
    public DateTime? ConvertedAt { get; private set; }

    private SubscriptionTrial() { }

    public static SubscriptionTrial Create(
        Guid subscriptionId,
        Guid instituteId,
        Guid planId,
        DateTime startDate,
        DateTime endDate)
    {
        var trial = new SubscriptionTrial
        {
            SubscriptionId = subscriptionId,
            InstituteId = instituteId,
            PlanId = planId,
            StartDate = startDate,
            EndDate = endDate,
            Status = TrialStatus.Active
        };

        trial.AddDomainEvent(new TrialStartedEvent
        {
            AggregateId = trial.Id,
            EventType = nameof(TrialStartedEvent),
            InstituteId = instituteId,
            TrialEndDate = endDate
        });

        return trial;
    }

    public void Convert()
    {
        Status = TrialStatus.Converted;
        ConvertedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new TrialConvertedEvent
        {
            AggregateId = Id,
            EventType = nameof(TrialConvertedEvent),
            InstituteId = InstituteId,
            SubscriptionId = SubscriptionId
        });
    }

    public void Expire()
    {
        Status = TrialStatus.Expired;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new TrialExpiredEvent
        {
            AggregateId = Id,
            EventType = nameof(TrialExpiredEvent),
            InstituteId = InstituteId
        });
    }

    public void Cancel()
    {
        Status = TrialStatus.Cancelled;
        UpdatedAt = DateTime.UtcNow;
    }

    protected override void When(DomainEvent @event)
    {
        // Event sourcing pattern - not implemented yet
    }
}
