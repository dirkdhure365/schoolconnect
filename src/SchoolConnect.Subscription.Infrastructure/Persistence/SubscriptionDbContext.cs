using MongoDB.Driver;
using SchoolConnect.Subscription.Domain.Entities;

namespace SchoolConnect.Subscription.Infrastructure.Persistence;

public class SubscriptionDbContext
{
    private readonly IMongoDatabase _database;

    public SubscriptionDbContext(string connectionString, string databaseName)
    {
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
    }

    public IMongoCollection<SubscriptionPlan> SubscriptionPlans =>
        _database.GetCollection<SubscriptionPlan>("subscription_plans");

    public IMongoCollection<Domain.Entities.Subscription> Subscriptions =>
        _database.GetCollection<Domain.Entities.Subscription>("subscriptions");

    public IMongoCollection<SubscriptionUsage> SubscriptionUsages =>
        _database.GetCollection<SubscriptionUsage>("subscription_usage");

    public IMongoCollection<SubscriptionTrial> SubscriptionTrials =>
        _database.GetCollection<SubscriptionTrial>("subscription_trials");
}
