using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.Subscription.Domain.Enums;
using SchoolConnect.Subscription.Domain.Events;

namespace SchoolConnect.Subscription.Domain.Entities;

public class SubscriptionUsage : Entity
{
    public Guid SubscriptionId { get; private set; }
    public Guid InstituteId { get; private set; }
    public int PeriodMonth { get; private set; }
    public int PeriodYear { get; private set; }
    public Dictionary<string, int> UsageMetrics { get; private set; } = new();
    public DateTime RecordedAt { get; private set; }

    private SubscriptionUsage() { }

    public static SubscriptionUsage Create(
        Guid subscriptionId,
        Guid instituteId,
        int periodMonth,
        int periodYear)
    {
        return new SubscriptionUsage
        {
            SubscriptionId = subscriptionId,
            InstituteId = instituteId,
            PeriodMonth = periodMonth,
            PeriodYear = periodYear,
            UsageMetrics = new Dictionary<string, int>(),
            RecordedAt = DateTime.UtcNow
        };
    }

    public void RecordMetric(FeatureType featureType, int usage)
    {
        var key = featureType.ToString();
        UsageMetrics[key] = usage;
        RecordedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public int GetMetric(FeatureType featureType)
    {
        var key = featureType.ToString();
        return UsageMetrics.TryGetValue(key, out var value) ? value : 0;
    }
}
