using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using SchoolConnect.Billing.Domain.Interfaces;
using SchoolConnect.Billing.Infrastructure.Persistence;
using SchoolConnect.Billing.Infrastructure.Repositories;

namespace SchoolConnect.Billing.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBillingInfrastructure(
        this IServiceCollection services,
        string connectionString,
        string databaseName)
    {
        // Register MongoDB client
        services.AddSingleton<IMongoClient>(sp =>
        {
            return new MongoClient(connectionString);
        });

        // Register DbContext
        services.AddSingleton(sp => new BillingDbContext(connectionString, databaseName));

        // Register Repositories
        services.AddSingleton<IBillingAccountRepository, BillingAccountRepository>();
        services.AddSingleton<IInvoiceRepository, InvoiceRepository>();
        services.AddSingleton<IPaymentRepository, PaymentRepository>();
        services.AddSingleton<IPaymentMethodRepository, PaymentMethodRepository>();
        services.AddSingleton<ITransactionRepository, TransactionRepository>();
        services.AddSingleton<ICreditNoteRepository, CreditNoteRepository>();

        return services;
    }
}
