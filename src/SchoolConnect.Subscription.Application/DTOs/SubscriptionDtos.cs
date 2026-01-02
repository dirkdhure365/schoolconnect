using SchoolConnect.Subscription.Domain.Enums;

namespace SchoolConnect.Subscription.Application.DTOs;

public record SubscriptionPlanDto(
    Guid Id,
    string Name,
    string Code,
    string? Description,
    PlanTier Tier,
    decimal MonthlyPrice,
    decimal AnnualPrice,
    string Currency,
    List<PlanFeatureDto> Features,
    bool IsActive,
    bool IsPublic,
    int TrialDays,
    DateTime CreatedAt,
    DateTime UpdatedAt
);

public record PlanFeatureDto(
    Guid Id,
    Guid PlanId,
    string FeatureCode,
    string FeatureName,
    FeatureType FeatureType,
    int LimitValue,
    bool IsUnlimited,
    bool IsEnabled
);

public record SubscriptionDto(
    Guid Id,
    Guid InstituteId,
    Guid PlanId,
    SubscriptionStatus Status,
    BillingFrequency BillingFrequency,
    DateTime StartDate,
    DateTime? EndDate,
    DateTime? NextBillingDate,
    DateTime? TrialStartDate,
    DateTime? TrialEndDate,
    bool AutoRenew,
    DateTime? CancelledAt,
    string? CancellationReason,
    DateTime CreatedAt,
    DateTime UpdatedAt
);

public record SubscriptionUsageDto(
    Guid Id,
    Guid SubscriptionId,
    Guid InstituteId,
    int PeriodMonth,
    int PeriodYear,
    Dictionary<string, int> UsageMetrics,
    DateTime RecordedAt
);

public record SubscriptionTrialDto(
    Guid Id,
    Guid SubscriptionId,
    Guid InstituteId,
    Guid PlanId,
    DateTime StartDate,
    DateTime EndDate,
    TrialStatus Status,
    DateTime? ConvertedAt,
    DateTime CreatedAt,
    DateTime UpdatedAt
);
