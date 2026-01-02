using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.Subscription.Domain.Enums;
using SchoolConnect.Subscription.Domain.Events;
using SchoolConnect.Subscription.Domain.ValueObjects;

namespace SchoolConnect.Subscription.Domain.Entities;

public class SubscriptionPlan : AggregateRoot
{
    public string Name { get; private set; } = string.Empty;
    public string Code { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public PlanTier Tier { get; private set; }
    public PlanPricing Pricing { get; private set; } = null!;
    public List<PlanFeature> Features { get; private set; } = new();
    public bool IsActive { get; private set; }
    public bool IsPublic { get; private set; }
    public int TrialDays { get; private set; }

    private SubscriptionPlan() { }

    public static SubscriptionPlan Create(
        string name,
        string code,
        string? description,
        PlanTier tier,
        PlanPricing pricing,
        int trialDays,
        bool isPublic = true)
    {
        var plan = new SubscriptionPlan
        {
            Name = name,
            Code = code,
            Description = description,
            Tier = tier,
            Pricing = pricing,
            TrialDays = trialDays,
            IsActive = true,
            IsPublic = isPublic,
            Features = new List<PlanFeature>()
        };

        plan.AddDomainEvent(new PlanCreatedEvent
        {
            AggregateId = plan.Id,
            EventType = nameof(PlanCreatedEvent),
            Name = name,
            Code = code,
            Tier = tier
        });

        return plan;
    }

    public void Update(
        string name,
        string? description,
        PlanPricing pricing,
        int trialDays,
        bool isPublic)
    {
        Name = name;
        Description = description;
        Pricing = pricing;
        TrialDays = trialDays;
        IsPublic = isPublic;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddFeature(PlanFeature feature)
    {
        Features.Add(feature);
        UpdatedAt = DateTime.UtcNow;
    }

    public void RemoveFeature(Guid featureId)
    {
        var feature = Features.FirstOrDefault(f => f.Id == featureId);
        if (feature != null)
        {
            Features.Remove(feature);
            UpdatedAt = DateTime.UtcNow;
        }
    }

    public void Activate()
    {
        IsActive = true;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new PlanDeactivatedEvent
        {
            AggregateId = Id,
            EventType = nameof(PlanDeactivatedEvent)
        });
    }

    protected override void When(DomainEvent @event)
    {
        // Event sourcing pattern - not implemented yet
    }
}
