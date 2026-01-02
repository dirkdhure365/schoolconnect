using MediatR;
using SchoolConnect.Subscription.Application.DTOs;

namespace SchoolConnect.Subscription.Application.Queries;

// Plan Queries
public record GetSubscriptionPlanByIdQuery(
    Guid Id
) : IRequest<SubscriptionPlanDto?>;

public record GetAllPlansQuery() : IRequest<IEnumerable<SubscriptionPlanDto>>;

public record GetActivePlansQuery() : IRequest<IEnumerable<SubscriptionPlanDto>>;

// Subscription Queries
public record GetSubscriptionByIdQuery(
    Guid Id
) : IRequest<SubscriptionDto?>;

public record GetSubscriptionByInstituteQuery(
    Guid InstituteId
) : IRequest<SubscriptionDto?>;

// Usage Queries
public record GetSubscriptionUsageQuery(
    Guid SubscriptionId,
    int? Month = null,
    int? Year = null
) : IRequest<IEnumerable<SubscriptionUsageDto>>;

// Trial Queries
public record GetTrialStatusQuery(
    Guid SubscriptionId
) : IRequest<SubscriptionTrialDto?>;
