using SchoolConnect.Subscription.Domain.Entities;

namespace SchoolConnect.Subscription.Domain.Interfaces;

public interface ISubscriptionPlanRepository
{
    Task<SubscriptionPlan?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<SubscriptionPlan?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
    Task<List<SubscriptionPlan>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<List<SubscriptionPlan>> GetActiveAsync(CancellationToken cancellationToken = default);
    Task<SubscriptionPlan> AddAsync(SubscriptionPlan plan, CancellationToken cancellationToken = default);
    Task UpdateAsync(SubscriptionPlan plan, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}

public interface ISubscriptionRepository
{
    Task<Entities.Subscription?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Entities.Subscription?> GetByInstituteIdAsync(Guid instituteId, CancellationToken cancellationToken = default);
    Task<List<Entities.Subscription>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Entities.Subscription> AddAsync(Entities.Subscription subscription, CancellationToken cancellationToken = default);
    Task UpdateAsync(Entities.Subscription subscription, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}

public interface ISubscriptionUsageRepository
{
    Task<SubscriptionUsage?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<SubscriptionUsage?> GetBySubscriptionAndPeriodAsync(Guid subscriptionId, int month, int year, CancellationToken cancellationToken = default);
    Task<List<SubscriptionUsage>> GetBySubscriptionIdAsync(Guid subscriptionId, CancellationToken cancellationToken = default);
    Task<SubscriptionUsage> AddAsync(SubscriptionUsage usage, CancellationToken cancellationToken = default);
    Task UpdateAsync(SubscriptionUsage usage, CancellationToken cancellationToken = default);
}

public interface ISubscriptionTrialRepository
{
    Task<SubscriptionTrial?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<SubscriptionTrial?> GetBySubscriptionIdAsync(Guid subscriptionId, CancellationToken cancellationToken = default);
    Task<SubscriptionTrial?> GetByInstituteIdAsync(Guid instituteId, CancellationToken cancellationToken = default);
    Task<List<SubscriptionTrial>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<SubscriptionTrial> AddAsync(SubscriptionTrial trial, CancellationToken cancellationToken = default);
    Task UpdateAsync(SubscriptionTrial trial, CancellationToken cancellationToken = default);
}
