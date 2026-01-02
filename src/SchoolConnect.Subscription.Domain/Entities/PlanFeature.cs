using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.Subscription.Domain.Enums;
using SchoolConnect.Subscription.Domain.ValueObjects;

namespace SchoolConnect.Subscription.Domain.Entities;

public class PlanFeature : Entity
{
    public Guid PlanId { get; private set; }
    public string FeatureCode { get; private set; } = string.Empty;
    public string FeatureName { get; private set; } = string.Empty;
    public FeatureType FeatureType { get; private set; }
    public FeatureLimit Limit { get; private set; } = null!;
    public bool IsEnabled { get; private set; }

    private PlanFeature() { }

    public static PlanFeature Create(
        Guid planId,
        string featureCode,
        string featureName,
        FeatureType featureType,
        FeatureLimit limit,
        bool isEnabled = true)
    {
        return new PlanFeature
        {
            PlanId = planId,
            FeatureCode = featureCode,
            FeatureName = featureName,
            FeatureType = featureType,
            Limit = limit,
            IsEnabled = isEnabled
        };
    }

    public void UpdateLimit(FeatureLimit limit)
    {
        Limit = limit;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Enable()
    {
        IsEnabled = true;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Disable()
    {
        IsEnabled = false;
        UpdatedAt = DateTime.UtcNow;
    }
}
