using MongoDB.Driver;
using SchoolConnect.Subscription.Domain.Entities;
using SchoolConnect.Subscription.Domain.Interfaces;
using SchoolConnect.Subscription.Infrastructure.Persistence;

namespace SchoolConnect.Subscription.Infrastructure.Repositories;

public class SubscriptionPlanRepository : ISubscriptionPlanRepository
{
    private readonly SubscriptionDbContext _context;

    public SubscriptionPlanRepository(SubscriptionDbContext context)
    {
        _context = context;
    }

    public async Task<SubscriptionPlan?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.SubscriptionPlans
            .Find(p => p.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<SubscriptionPlan?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        return await _context.SubscriptionPlans
            .Find(p => p.Code == code)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<SubscriptionPlan>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SubscriptionPlans
            .Find(_ => true)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<SubscriptionPlan>> GetActiveAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SubscriptionPlans
            .Find(p => p.IsActive)
            .ToListAsync(cancellationToken);
    }

    public async Task<SubscriptionPlan> AddAsync(SubscriptionPlan plan, CancellationToken cancellationToken = default)
    {
        await _context.SubscriptionPlans.InsertOneAsync(plan, cancellationToken: cancellationToken);
        return plan;
    }

    public async Task UpdateAsync(SubscriptionPlan plan, CancellationToken cancellationToken = default)
    {
        await _context.SubscriptionPlans.ReplaceOneAsync(
            p => p.Id == plan.Id,
            plan,
            cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _context.SubscriptionPlans.DeleteOneAsync(
            p => p.Id == id,
            cancellationToken);
    }
}

public class SubscriptionRepository : ISubscriptionRepository
{
    private readonly SubscriptionDbContext _context;

    public SubscriptionRepository(SubscriptionDbContext context)
    {
        _context = context;
    }

    public async Task<Domain.Entities.Subscription?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Subscriptions
            .Find(s => s.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Domain.Entities.Subscription?> GetByInstituteIdAsync(Guid instituteId, CancellationToken cancellationToken = default)
    {
        return await _context.Subscriptions
            .Find(s => s.InstituteId == instituteId)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<Domain.Entities.Subscription>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Subscriptions
            .Find(_ => true)
            .ToListAsync(cancellationToken);
    }

    public async Task<Domain.Entities.Subscription> AddAsync(Domain.Entities.Subscription subscription, CancellationToken cancellationToken = default)
    {
        await _context.Subscriptions.InsertOneAsync(subscription, cancellationToken: cancellationToken);
        return subscription;
    }

    public async Task UpdateAsync(Domain.Entities.Subscription subscription, CancellationToken cancellationToken = default)
    {
        await _context.Subscriptions.ReplaceOneAsync(
            s => s.Id == subscription.Id,
            subscription,
            cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _context.Subscriptions.DeleteOneAsync(
            s => s.Id == id,
            cancellationToken);
    }
}

public class SubscriptionUsageRepository : ISubscriptionUsageRepository
{
    private readonly SubscriptionDbContext _context;

    public SubscriptionUsageRepository(SubscriptionDbContext context)
    {
        _context = context;
    }

    public async Task<SubscriptionUsage?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.SubscriptionUsages
            .Find(u => u.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<SubscriptionUsage?> GetBySubscriptionAndPeriodAsync(Guid subscriptionId, int month, int year, CancellationToken cancellationToken = default)
    {
        return await _context.SubscriptionUsages
            .Find(u => u.SubscriptionId == subscriptionId && u.PeriodMonth == month && u.PeriodYear == year)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<SubscriptionUsage>> GetBySubscriptionIdAsync(Guid subscriptionId, CancellationToken cancellationToken = default)
    {
        return await _context.SubscriptionUsages
            .Find(u => u.SubscriptionId == subscriptionId)
            .ToListAsync(cancellationToken);
    }

    public async Task<SubscriptionUsage> AddAsync(SubscriptionUsage usage, CancellationToken cancellationToken = default)
    {
        await _context.SubscriptionUsages.InsertOneAsync(usage, cancellationToken: cancellationToken);
        return usage;
    }

    public async Task UpdateAsync(SubscriptionUsage usage, CancellationToken cancellationToken = default)
    {
        await _context.SubscriptionUsages.ReplaceOneAsync(
            u => u.Id == usage.Id,
            usage,
            cancellationToken: cancellationToken);
    }
}

public class SubscriptionTrialRepository : ISubscriptionTrialRepository
{
    private readonly SubscriptionDbContext _context;

    public SubscriptionTrialRepository(SubscriptionDbContext context)
    {
        _context = context;
    }

    public async Task<SubscriptionTrial?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.SubscriptionTrials
            .Find(t => t.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<SubscriptionTrial?> GetBySubscriptionIdAsync(Guid subscriptionId, CancellationToken cancellationToken = default)
    {
        return await _context.SubscriptionTrials
            .Find(t => t.SubscriptionId == subscriptionId)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<SubscriptionTrial?> GetByInstituteIdAsync(Guid instituteId, CancellationToken cancellationToken = default)
    {
        return await _context.SubscriptionTrials
            .Find(t => t.InstituteId == instituteId)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<SubscriptionTrial>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SubscriptionTrials
            .Find(_ => true)
            .ToListAsync(cancellationToken);
    }

    public async Task<SubscriptionTrial> AddAsync(SubscriptionTrial trial, CancellationToken cancellationToken = default)
    {
        await _context.SubscriptionTrials.InsertOneAsync(trial, cancellationToken: cancellationToken);
        return trial;
    }

    public async Task UpdateAsync(SubscriptionTrial trial, CancellationToken cancellationToken = default)
    {
        await _context.SubscriptionTrials.ReplaceOneAsync(
            t => t.Id == trial.Id,
            trial,
            cancellationToken: cancellationToken);
    }
}
