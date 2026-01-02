using MongoDB.Driver;
using SchoolConnect.Billing.Domain.Entities;

namespace SchoolConnect.Billing.Infrastructure.Persistence;

public class BillingDbContext
{
    private readonly IMongoDatabase _database;

    public BillingDbContext(string connectionString, string databaseName)
    {
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
    }

    public IMongoCollection<BillingAccount> BillingAccounts =>
        _database.GetCollection<BillingAccount>("billing_accounts");

    public IMongoCollection<Invoice> Invoices =>
        _database.GetCollection<Invoice>("invoices");

    public IMongoCollection<Payment> Payments =>
        _database.GetCollection<Payment>("payments");

    public IMongoCollection<PaymentMethod> PaymentMethods =>
        _database.GetCollection<PaymentMethod>("payment_methods");

    public IMongoCollection<Transaction> Transactions =>
        _database.GetCollection<Transaction>("transactions");

    public IMongoCollection<CreditNote> CreditNotes =>
        _database.GetCollection<CreditNote>("credit_notes");
}
