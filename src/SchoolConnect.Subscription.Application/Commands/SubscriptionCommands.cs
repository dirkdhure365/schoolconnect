using MediatR;
using SchoolConnect.Subscription.Application.DTOs;
using SchoolConnect.Subscription.Domain.Enums;

namespace SchoolConnect.Subscription.Application.Commands;

// Plan Commands
public record CreateSubscriptionPlanCommand(
    string Name,
    string Code,
    string? Description,
    PlanTier Tier,
    decimal MonthlyPrice,
    decimal AnnualPrice,
    string Currency,
    int TrialDays,
    bool IsPublic = true
) : IRequest<SubscriptionPlanDto>;

public record UpdateSubscriptionPlanCommand(
    Guid Id,
    string Name,
    string? Description,
    decimal MonthlyPrice,
    decimal AnnualPrice,
    string Currency,
    int TrialDays,
    bool IsPublic
) : IRequest<SubscriptionPlanDto>;

public record DeactivatePlanCommand(
    Guid Id
) : IRequest<bool>;

// Subscription Commands
public record CreateSubscriptionCommand(
    Guid InstituteId,
    Guid PlanId,
    BillingFrequency BillingFrequency,
    DateTime StartDate,
    bool AutoRenew = true
) : IRequest<SubscriptionDto>;

public record UpgradeSubscriptionCommand(
    Guid Id,
    Guid NewPlanId
) : IRequest<SubscriptionDto>;

public record DowngradeSubscriptionCommand(
    Guid Id,
    Guid NewPlanId
) : IRequest<SubscriptionDto>;

public record CancelSubscriptionCommand(
    Guid Id,
    string? Reason = null
) : IRequest<SubscriptionDto>;

public record RenewSubscriptionCommand(
    Guid Id
) : IRequest<SubscriptionDto>;

// Trial Commands
public record StartTrialCommand(
    Guid SubscriptionId,
    Guid InstituteId,
    Guid PlanId,
    int TrialDays
) : IRequest<SubscriptionTrialDto>;

public record ConvertTrialCommand(
    Guid TrialId
) : IRequest<SubscriptionTrialDto>;

// Usage Commands
public record RecordUsageCommand(
    Guid SubscriptionId,
    Guid InstituteId,
    FeatureType FeatureType,
    int Usage
) : IRequest<SubscriptionUsageDto>;
