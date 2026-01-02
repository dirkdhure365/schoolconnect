using MediatR;
using SchoolConnect.Subscription.Application.Commands;
using SchoolConnect.Subscription.Application.DTOs;
using SchoolConnect.Subscription.Application.Queries;
using SchoolConnect.Subscription.Domain.Entities;
using SchoolConnect.Subscription.Domain.Interfaces;
using SchoolConnect.Subscription.Domain.ValueObjects;

namespace SchoolConnect.Subscription.Application.Handlers;

// ===================== PLAN HANDLERS =====================

public class CreateSubscriptionPlanHandler : IRequestHandler<CreateSubscriptionPlanCommand, SubscriptionPlanDto>
{
    private readonly ISubscriptionPlanRepository _repository;

    public CreateSubscriptionPlanHandler(ISubscriptionPlanRepository repository)
    {
        _repository = repository;
    }

    public async Task<SubscriptionPlanDto> Handle(CreateSubscriptionPlanCommand request, CancellationToken cancellationToken)
    {
        var pricing = new PlanPricing(request.MonthlyPrice, request.AnnualPrice, request.Currency);
        
        var plan = SubscriptionPlan.Create(
            request.Name,
            request.Code,
            request.Description,
            request.Tier,
            pricing,
            request.TrialDays,
            request.IsPublic
        );

        await _repository.AddAsync(plan, cancellationToken);
        return MapPlanToDto(plan);
    }

    private static SubscriptionPlanDto MapPlanToDto(SubscriptionPlan plan) => new(
        plan.Id,
        plan.Name,
        plan.Code,
        plan.Description,
        plan.Tier,
        plan.Pricing.MonthlyPrice,
        plan.Pricing.AnnualPrice,
        plan.Pricing.Currency,
        plan.Features.Select(f => new PlanFeatureDto(
            f.Id,
            f.PlanId,
            f.FeatureCode,
            f.FeatureName,
            f.FeatureType,
            f.Limit.Value,
            f.Limit.IsUnlimited,
            f.IsEnabled
        )).ToList(),
        plan.IsActive,
        plan.IsPublic,
        plan.TrialDays,
        plan.CreatedAt,
        plan.UpdatedAt
    );
}

public class UpdateSubscriptionPlanHandler : IRequestHandler<UpdateSubscriptionPlanCommand, SubscriptionPlanDto>
{
    private readonly ISubscriptionPlanRepository _repository;

    public UpdateSubscriptionPlanHandler(ISubscriptionPlanRepository repository)
    {
        _repository = repository;
    }

    public async Task<SubscriptionPlanDto> Handle(UpdateSubscriptionPlanCommand request, CancellationToken cancellationToken)
    {
        var plan = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (plan == null) throw new Exception($"Subscription plan not found: {request.Id}");

        var pricing = new PlanPricing(request.MonthlyPrice, request.AnnualPrice, request.Currency);
        plan.Update(request.Name, request.Description, pricing, request.TrialDays, request.IsPublic);

        await _repository.UpdateAsync(plan, cancellationToken);
        return MapPlanToDto(plan);
    }

    private static SubscriptionPlanDto MapPlanToDto(SubscriptionPlan plan) => new(
        plan.Id,
        plan.Name,
        plan.Code,
        plan.Description,
        plan.Tier,
        plan.Pricing.MonthlyPrice,
        plan.Pricing.AnnualPrice,
        plan.Pricing.Currency,
        plan.Features.Select(f => new PlanFeatureDto(
            f.Id,
            f.PlanId,
            f.FeatureCode,
            f.FeatureName,
            f.FeatureType,
            f.Limit.Value,
            f.Limit.IsUnlimited,
            f.IsEnabled
        )).ToList(),
        plan.IsActive,
        plan.IsPublic,
        plan.TrialDays,
        plan.CreatedAt,
        plan.UpdatedAt
    );
}

public class DeactivatePlanHandler : IRequestHandler<DeactivatePlanCommand, bool>
{
    private readonly ISubscriptionPlanRepository _repository;

    public DeactivatePlanHandler(ISubscriptionPlanRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeactivatePlanCommand request, CancellationToken cancellationToken)
    {
        var plan = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (plan == null) return false;

        plan.Deactivate();
        await _repository.UpdateAsync(plan, cancellationToken);
        return true;
    }
}

public class GetSubscriptionPlanByIdHandler : IRequestHandler<GetSubscriptionPlanByIdQuery, SubscriptionPlanDto?>
{
    private readonly ISubscriptionPlanRepository _repository;

    public GetSubscriptionPlanByIdHandler(ISubscriptionPlanRepository repository)
    {
        _repository = repository;
    }

    public async Task<SubscriptionPlanDto?> Handle(GetSubscriptionPlanByIdQuery request, CancellationToken cancellationToken)
    {
        var plan = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (plan == null) return null;
        return MapPlanToDto(plan);
    }

    private static SubscriptionPlanDto MapPlanToDto(SubscriptionPlan plan) => new(
        plan.Id,
        plan.Name,
        plan.Code,
        plan.Description,
        plan.Tier,
        plan.Pricing.MonthlyPrice,
        plan.Pricing.AnnualPrice,
        plan.Pricing.Currency,
        plan.Features.Select(f => new PlanFeatureDto(
            f.Id,
            f.PlanId,
            f.FeatureCode,
            f.FeatureName,
            f.FeatureType,
            f.Limit.Value,
            f.Limit.IsUnlimited,
            f.IsEnabled
        )).ToList(),
        plan.IsActive,
        plan.IsPublic,
        plan.TrialDays,
        plan.CreatedAt,
        plan.UpdatedAt
    );
}

public class GetAllPlansHandler : IRequestHandler<GetAllPlansQuery, IEnumerable<SubscriptionPlanDto>>
{
    private readonly ISubscriptionPlanRepository _repository;

    public GetAllPlansHandler(ISubscriptionPlanRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<SubscriptionPlanDto>> Handle(GetAllPlansQuery request, CancellationToken cancellationToken)
    {
        var plans = await _repository.GetAllAsync(cancellationToken);
        return plans.Select(MapPlanToDto);
    }

    private static SubscriptionPlanDto MapPlanToDto(SubscriptionPlan plan) => new(
        plan.Id,
        plan.Name,
        plan.Code,
        plan.Description,
        plan.Tier,
        plan.Pricing.MonthlyPrice,
        plan.Pricing.AnnualPrice,
        plan.Pricing.Currency,
        plan.Features.Select(f => new PlanFeatureDto(
            f.Id,
            f.PlanId,
            f.FeatureCode,
            f.FeatureName,
            f.FeatureType,
            f.Limit.Value,
            f.Limit.IsUnlimited,
            f.IsEnabled
        )).ToList(),
        plan.IsActive,
        plan.IsPublic,
        plan.TrialDays,
        plan.CreatedAt,
        plan.UpdatedAt
    );
}

public class GetActivePlansHandler : IRequestHandler<GetActivePlansQuery, IEnumerable<SubscriptionPlanDto>>
{
    private readonly ISubscriptionPlanRepository _repository;

    public GetActivePlansHandler(ISubscriptionPlanRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<SubscriptionPlanDto>> Handle(GetActivePlansQuery request, CancellationToken cancellationToken)
    {
        var plans = await _repository.GetActiveAsync(cancellationToken);
        return plans.Select(MapPlanToDto);
    }

    private static SubscriptionPlanDto MapPlanToDto(SubscriptionPlan plan) => new(
        plan.Id,
        plan.Name,
        plan.Code,
        plan.Description,
        plan.Tier,
        plan.Pricing.MonthlyPrice,
        plan.Pricing.AnnualPrice,
        plan.Pricing.Currency,
        plan.Features.Select(f => new PlanFeatureDto(
            f.Id,
            f.PlanId,
            f.FeatureCode,
            f.FeatureName,
            f.FeatureType,
            f.Limit.Value,
            f.Limit.IsUnlimited,
            f.IsEnabled
        )).ToList(),
        plan.IsActive,
        plan.IsPublic,
        plan.TrialDays,
        plan.CreatedAt,
        plan.UpdatedAt
    );
}

// ===================== SUBSCRIPTION HANDLERS =====================

public class CreateSubscriptionHandler : IRequestHandler<CreateSubscriptionCommand, SubscriptionDto>
{
    private readonly ISubscriptionRepository _repository;

    public CreateSubscriptionHandler(ISubscriptionRepository repository)
    {
        _repository = repository;
    }

    public async Task<SubscriptionDto> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var subscription = Domain.Entities.Subscription.Create(
            request.InstituteId,
            request.PlanId,
            request.BillingFrequency,
            request.StartDate,
            request.AutoRenew
        );

        await _repository.AddAsync(subscription, cancellationToken);
        return MapSubscriptionToDto(subscription);
    }

    private static SubscriptionDto MapSubscriptionToDto(Domain.Entities.Subscription subscription) => new(
        subscription.Id,
        subscription.InstituteId,
        subscription.PlanId,
        subscription.Status,
        subscription.BillingFrequency,
        subscription.StartDate,
        subscription.EndDate,
        subscription.NextBillingDate,
        subscription.TrialStartDate,
        subscription.TrialEndDate,
        subscription.AutoRenew,
        subscription.CancelledAt,
        subscription.CancellationReason,
        subscription.CreatedAt,
        subscription.UpdatedAt
    );
}

public class UpgradeSubscriptionHandler : IRequestHandler<UpgradeSubscriptionCommand, SubscriptionDto>
{
    private readonly ISubscriptionRepository _repository;

    public UpgradeSubscriptionHandler(ISubscriptionRepository repository)
    {
        _repository = repository;
    }

    public async Task<SubscriptionDto> Handle(UpgradeSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var subscription = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (subscription == null) throw new Exception($"Subscription not found: {request.Id}");

        subscription.Upgrade(request.NewPlanId);
        await _repository.UpdateAsync(subscription, cancellationToken);
        return MapSubscriptionToDto(subscription);
    }

    private static SubscriptionDto MapSubscriptionToDto(Domain.Entities.Subscription subscription) => new(
        subscription.Id,
        subscription.InstituteId,
        subscription.PlanId,
        subscription.Status,
        subscription.BillingFrequency,
        subscription.StartDate,
        subscription.EndDate,
        subscription.NextBillingDate,
        subscription.TrialStartDate,
        subscription.TrialEndDate,
        subscription.AutoRenew,
        subscription.CancelledAt,
        subscription.CancellationReason,
        subscription.CreatedAt,
        subscription.UpdatedAt
    );
}

public class DowngradeSubscriptionHandler : IRequestHandler<DowngradeSubscriptionCommand, SubscriptionDto>
{
    private readonly ISubscriptionRepository _repository;

    public DowngradeSubscriptionHandler(ISubscriptionRepository repository)
    {
        _repository = repository;
    }

    public async Task<SubscriptionDto> Handle(DowngradeSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var subscription = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (subscription == null) throw new Exception($"Subscription not found: {request.Id}");

        subscription.Downgrade(request.NewPlanId);
        await _repository.UpdateAsync(subscription, cancellationToken);
        return MapSubscriptionToDto(subscription);
    }

    private static SubscriptionDto MapSubscriptionToDto(Domain.Entities.Subscription subscription) => new(
        subscription.Id,
        subscription.InstituteId,
        subscription.PlanId,
        subscription.Status,
        subscription.BillingFrequency,
        subscription.StartDate,
        subscription.EndDate,
        subscription.NextBillingDate,
        subscription.TrialStartDate,
        subscription.TrialEndDate,
        subscription.AutoRenew,
        subscription.CancelledAt,
        subscription.CancellationReason,
        subscription.CreatedAt,
        subscription.UpdatedAt
    );
}

public class CancelSubscriptionHandler : IRequestHandler<CancelSubscriptionCommand, SubscriptionDto>
{
    private readonly ISubscriptionRepository _repository;

    public CancelSubscriptionHandler(ISubscriptionRepository repository)
    {
        _repository = repository;
    }

    public async Task<SubscriptionDto> Handle(CancelSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var subscription = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (subscription == null) throw new Exception($"Subscription not found: {request.Id}");

        subscription.Cancel(request.Reason);
        await _repository.UpdateAsync(subscription, cancellationToken);
        return MapSubscriptionToDto(subscription);
    }

    private static SubscriptionDto MapSubscriptionToDto(Domain.Entities.Subscription subscription) => new(
        subscription.Id,
        subscription.InstituteId,
        subscription.PlanId,
        subscription.Status,
        subscription.BillingFrequency,
        subscription.StartDate,
        subscription.EndDate,
        subscription.NextBillingDate,
        subscription.TrialStartDate,
        subscription.TrialEndDate,
        subscription.AutoRenew,
        subscription.CancelledAt,
        subscription.CancellationReason,
        subscription.CreatedAt,
        subscription.UpdatedAt
    );
}

public class RenewSubscriptionHandler : IRequestHandler<RenewSubscriptionCommand, SubscriptionDto>
{
    private readonly ISubscriptionRepository _repository;

    public RenewSubscriptionHandler(ISubscriptionRepository repository)
    {
        _repository = repository;
    }

    public async Task<SubscriptionDto> Handle(RenewSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var subscription = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (subscription == null) throw new Exception($"Subscription not found: {request.Id}");

        subscription.Renew();
        await _repository.UpdateAsync(subscription, cancellationToken);
        return MapSubscriptionToDto(subscription);
    }

    private static SubscriptionDto MapSubscriptionToDto(Domain.Entities.Subscription subscription) => new(
        subscription.Id,
        subscription.InstituteId,
        subscription.PlanId,
        subscription.Status,
        subscription.BillingFrequency,
        subscription.StartDate,
        subscription.EndDate,
        subscription.NextBillingDate,
        subscription.TrialStartDate,
        subscription.TrialEndDate,
        subscription.AutoRenew,
        subscription.CancelledAt,
        subscription.CancellationReason,
        subscription.CreatedAt,
        subscription.UpdatedAt
    );
}

public class GetSubscriptionByIdHandler : IRequestHandler<GetSubscriptionByIdQuery, SubscriptionDto?>
{
    private readonly ISubscriptionRepository _repository;

    public GetSubscriptionByIdHandler(ISubscriptionRepository repository)
    {
        _repository = repository;
    }

    public async Task<SubscriptionDto?> Handle(GetSubscriptionByIdQuery request, CancellationToken cancellationToken)
    {
        var subscription = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (subscription == null) return null;
        return MapSubscriptionToDto(subscription);
    }

    private static SubscriptionDto MapSubscriptionToDto(Domain.Entities.Subscription subscription) => new(
        subscription.Id,
        subscription.InstituteId,
        subscription.PlanId,
        subscription.Status,
        subscription.BillingFrequency,
        subscription.StartDate,
        subscription.EndDate,
        subscription.NextBillingDate,
        subscription.TrialStartDate,
        subscription.TrialEndDate,
        subscription.AutoRenew,
        subscription.CancelledAt,
        subscription.CancellationReason,
        subscription.CreatedAt,
        subscription.UpdatedAt
    );
}

public class GetSubscriptionByInstituteHandler : IRequestHandler<GetSubscriptionByInstituteQuery, SubscriptionDto?>
{
    private readonly ISubscriptionRepository _repository;

    public GetSubscriptionByInstituteHandler(ISubscriptionRepository repository)
    {
        _repository = repository;
    }

    public async Task<SubscriptionDto?> Handle(GetSubscriptionByInstituteQuery request, CancellationToken cancellationToken)
    {
        var subscription = await _repository.GetByInstituteIdAsync(request.InstituteId, cancellationToken);
        if (subscription == null) return null;
        return MapSubscriptionToDto(subscription);
    }

    private static SubscriptionDto MapSubscriptionToDto(Domain.Entities.Subscription subscription) => new(
        subscription.Id,
        subscription.InstituteId,
        subscription.PlanId,
        subscription.Status,
        subscription.BillingFrequency,
        subscription.StartDate,
        subscription.EndDate,
        subscription.NextBillingDate,
        subscription.TrialStartDate,
        subscription.TrialEndDate,
        subscription.AutoRenew,
        subscription.CancelledAt,
        subscription.CancellationReason,
        subscription.CreatedAt,
        subscription.UpdatedAt
    );
}

// ===================== TRIAL HANDLERS =====================

public class StartTrialHandler : IRequestHandler<StartTrialCommand, SubscriptionTrialDto>
{
    private readonly ISubscriptionTrialRepository _repository;

    public StartTrialHandler(ISubscriptionTrialRepository repository)
    {
        _repository = repository;
    }

    public async Task<SubscriptionTrialDto> Handle(StartTrialCommand request, CancellationToken cancellationToken)
    {
        var startDate = DateTime.UtcNow;
        var endDate = startDate.AddDays(request.TrialDays);

        var trial = SubscriptionTrial.Create(
            request.SubscriptionId,
            request.InstituteId,
            request.PlanId,
            startDate,
            endDate
        );

        await _repository.AddAsync(trial, cancellationToken);
        return MapTrialToDto(trial);
    }

    private static SubscriptionTrialDto MapTrialToDto(SubscriptionTrial trial) => new(
        trial.Id,
        trial.SubscriptionId,
        trial.InstituteId,
        trial.PlanId,
        trial.StartDate,
        trial.EndDate,
        trial.Status,
        trial.ConvertedAt,
        trial.CreatedAt,
        trial.UpdatedAt
    );
}

public class ConvertTrialHandler : IRequestHandler<ConvertTrialCommand, SubscriptionTrialDto>
{
    private readonly ISubscriptionTrialRepository _repository;

    public ConvertTrialHandler(ISubscriptionTrialRepository repository)
    {
        _repository = repository;
    }

    public async Task<SubscriptionTrialDto> Handle(ConvertTrialCommand request, CancellationToken cancellationToken)
    {
        var trial = await _repository.GetByIdAsync(request.TrialId, cancellationToken);
        if (trial == null) throw new Exception($"Trial not found: {request.TrialId}");

        trial.Convert();
        await _repository.UpdateAsync(trial, cancellationToken);
        return MapTrialToDto(trial);
    }

    private static SubscriptionTrialDto MapTrialToDto(SubscriptionTrial trial) => new(
        trial.Id,
        trial.SubscriptionId,
        trial.InstituteId,
        trial.PlanId,
        trial.StartDate,
        trial.EndDate,
        trial.Status,
        trial.ConvertedAt,
        trial.CreatedAt,
        trial.UpdatedAt
    );
}

public class GetTrialStatusHandler : IRequestHandler<GetTrialStatusQuery, SubscriptionTrialDto?>
{
    private readonly ISubscriptionTrialRepository _repository;

    public GetTrialStatusHandler(ISubscriptionTrialRepository repository)
    {
        _repository = repository;
    }

    public async Task<SubscriptionTrialDto?> Handle(GetTrialStatusQuery request, CancellationToken cancellationToken)
    {
        var trial = await _repository.GetBySubscriptionIdAsync(request.SubscriptionId, cancellationToken);
        if (trial == null) return null;
        return MapTrialToDto(trial);
    }

    private static SubscriptionTrialDto MapTrialToDto(SubscriptionTrial trial) => new(
        trial.Id,
        trial.SubscriptionId,
        trial.InstituteId,
        trial.PlanId,
        trial.StartDate,
        trial.EndDate,
        trial.Status,
        trial.ConvertedAt,
        trial.CreatedAt,
        trial.UpdatedAt
    );
}

// ===================== USAGE HANDLERS =====================

public class RecordUsageHandler : IRequestHandler<RecordUsageCommand, SubscriptionUsageDto>
{
    private readonly ISubscriptionUsageRepository _repository;

    public RecordUsageHandler(ISubscriptionUsageRepository repository)
    {
        _repository = repository;
    }

    public async Task<SubscriptionUsageDto> Handle(RecordUsageCommand request, CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;
        var usage = await _repository.GetBySubscriptionAndPeriodAsync(
            request.SubscriptionId,
            now.Month,
            now.Year,
            cancellationToken
        );

        if (usage == null)
        {
            usage = SubscriptionUsage.Create(
                request.SubscriptionId,
                request.InstituteId,
                now.Month,
                now.Year
            );
            usage.RecordMetric(request.FeatureType, request.Usage);
            await _repository.AddAsync(usage, cancellationToken);
        }
        else
        {
            usage.RecordMetric(request.FeatureType, request.Usage);
            await _repository.UpdateAsync(usage, cancellationToken);
        }

        return MapUsageToDto(usage);
    }

    private static SubscriptionUsageDto MapUsageToDto(SubscriptionUsage usage) => new(
        usage.Id,
        usage.SubscriptionId,
        usage.InstituteId,
        usage.PeriodMonth,
        usage.PeriodYear,
        usage.UsageMetrics,
        usage.RecordedAt
    );
}

public class GetSubscriptionUsageHandler : IRequestHandler<GetSubscriptionUsageQuery, IEnumerable<SubscriptionUsageDto>>
{
    private readonly ISubscriptionUsageRepository _repository;

    public GetSubscriptionUsageHandler(ISubscriptionUsageRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<SubscriptionUsageDto>> Handle(GetSubscriptionUsageQuery request, CancellationToken cancellationToken)
    {
        if (request.Month.HasValue && request.Year.HasValue)
        {
            var usage = await _repository.GetBySubscriptionAndPeriodAsync(
                request.SubscriptionId,
                request.Month.Value,
                request.Year.Value,
                cancellationToken
            );
            return usage != null ? new[] { MapUsageToDto(usage) } : Array.Empty<SubscriptionUsageDto>();
        }

        var usages = await _repository.GetBySubscriptionIdAsync(request.SubscriptionId, cancellationToken);
        return usages.Select(MapUsageToDto);
    }

    private static SubscriptionUsageDto MapUsageToDto(SubscriptionUsage usage) => new(
        usage.Id,
        usage.SubscriptionId,
        usage.InstituteId,
        usage.PeriodMonth,
        usage.PeriodYear,
        usage.UsageMetrics,
        usage.RecordedAt
    );
}
