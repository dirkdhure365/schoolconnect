using MongoDB.Driver;
using SchoolConnect.Billing.Domain.Entities;
using SchoolConnect.Billing.Domain.Enums;
using SchoolConnect.Billing.Domain.Interfaces;
using SchoolConnect.Billing.Infrastructure.Persistence;

namespace SchoolConnect.Billing.Infrastructure.Repositories;

public class BillingAccountRepository : IBillingAccountRepository
{
    private readonly BillingDbContext _context;

    public BillingAccountRepository(BillingDbContext context)
    {
        _context = context;
    }

    public async Task<BillingAccount?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.BillingAccounts.Find(a => a.Id == id).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<BillingAccount?> GetByInstituteIdAsync(Guid instituteId, CancellationToken cancellationToken = default)
    {
        return await _context.BillingAccounts.Find(a => a.InstituteId == instituteId).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<BillingAccount>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.BillingAccounts.Find(_ => true).ToListAsync(cancellationToken);
    }

    public async Task<BillingAccount> AddAsync(BillingAccount account, CancellationToken cancellationToken = default)
    {
        await _context.BillingAccounts.InsertOneAsync(account, cancellationToken: cancellationToken);
        return account;
    }

    public async Task UpdateAsync(BillingAccount account, CancellationToken cancellationToken = default)
    {
        await _context.BillingAccounts.ReplaceOneAsync(a => a.Id == account.Id, account, cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _context.BillingAccounts.DeleteOneAsync(a => a.Id == id, cancellationToken);
    }
}

public class InvoiceRepository : IInvoiceRepository
{
    private readonly BillingDbContext _context;

    public InvoiceRepository(BillingDbContext context)
    {
        _context = context;
    }

    public async Task<Invoice?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Invoices.Find(i => i.Id == id).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Invoice?> GetByInvoiceNumberAsync(string invoiceNumber, CancellationToken cancellationToken = default)
    {
        return await _context.Invoices.Find(i => i.InvoiceNumber == invoiceNumber).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<Invoice>> GetByBillingAccountIdAsync(Guid billingAccountId, CancellationToken cancellationToken = default)
    {
        return await _context.Invoices.Find(i => i.BillingAccountId == billingAccountId).ToListAsync(cancellationToken);
    }

    public async Task<List<Invoice>> GetByStatusAsync(InvoiceStatus status, CancellationToken cancellationToken = default)
    {
        return await _context.Invoices.Find(i => i.Status == status).ToListAsync(cancellationToken);
    }

    public async Task<List<Invoice>> GetOverdueInvoicesAsync(CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;
        return await _context.Invoices.Find(i => i.Status == InvoiceStatus.Sent && i.DueDate < now).ToListAsync(cancellationToken);
    }

    public async Task<List<Invoice>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Invoices.Find(_ => true).ToListAsync(cancellationToken);
    }

    public async Task<Invoice> AddAsync(Invoice invoice, CancellationToken cancellationToken = default)
    {
        await _context.Invoices.InsertOneAsync(invoice, cancellationToken: cancellationToken);
        return invoice;
    }

    public async Task UpdateAsync(Invoice invoice, CancellationToken cancellationToken = default)
    {
        await _context.Invoices.ReplaceOneAsync(i => i.Id == invoice.Id, invoice, cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _context.Invoices.DeleteOneAsync(i => i.Id == id, cancellationToken);
    }
}

public class PaymentRepository : IPaymentRepository
{
    private readonly BillingDbContext _context;

    public PaymentRepository(BillingDbContext context)
    {
        _context = context;
    }

    public async Task<Payment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Payments.Find(p => p.Id == id).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<Payment>> GetByInvoiceIdAsync(Guid invoiceId, CancellationToken cancellationToken = default)
    {
        return await _context.Payments.Find(p => p.InvoiceId == invoiceId).ToListAsync(cancellationToken);
    }

    public async Task<List<Payment>> GetByBillingAccountIdAsync(Guid billingAccountId, CancellationToken cancellationToken = default)
    {
        return await _context.Payments.Find(p => p.BillingAccountId == billingAccountId).ToListAsync(cancellationToken);
    }

    public async Task<List<Payment>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Payments.Find(_ => true).ToListAsync(cancellationToken);
    }

    public async Task<Payment> AddAsync(Payment payment, CancellationToken cancellationToken = default)
    {
        await _context.Payments.InsertOneAsync(payment, cancellationToken: cancellationToken);
        return payment;
    }

    public async Task UpdateAsync(Payment payment, CancellationToken cancellationToken = default)
    {
        await _context.Payments.ReplaceOneAsync(p => p.Id == payment.Id, payment, cancellationToken: cancellationToken);
    }
}

public class PaymentMethodRepository : IPaymentMethodRepository
{
    private readonly BillingDbContext _context;

    public PaymentMethodRepository(BillingDbContext context)
    {
        _context = context;
    }

    public async Task<PaymentMethod?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.PaymentMethods.Find(pm => pm.Id == id).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<PaymentMethod>> GetByBillingAccountIdAsync(Guid billingAccountId, CancellationToken cancellationToken = default)
    {
        return await _context.PaymentMethods.Find(pm => pm.BillingAccountId == billingAccountId && pm.IsActive).ToListAsync(cancellationToken);
    }

    public async Task<PaymentMethod?> GetDefaultByBillingAccountIdAsync(Guid billingAccountId, CancellationToken cancellationToken = default)
    {
        return await _context.PaymentMethods.Find(pm => pm.BillingAccountId == billingAccountId && pm.IsDefault && pm.IsActive).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<PaymentMethod>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.PaymentMethods.Find(_ => true).ToListAsync(cancellationToken);
    }

    public async Task<PaymentMethod> AddAsync(PaymentMethod paymentMethod, CancellationToken cancellationToken = default)
    {
        await _context.PaymentMethods.InsertOneAsync(paymentMethod, cancellationToken: cancellationToken);
        return paymentMethod;
    }

    public async Task UpdateAsync(PaymentMethod paymentMethod, CancellationToken cancellationToken = default)
    {
        await _context.PaymentMethods.ReplaceOneAsync(pm => pm.Id == paymentMethod.Id, paymentMethod, cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _context.PaymentMethods.DeleteOneAsync(pm => pm.Id == id, cancellationToken);
    }
}

public class TransactionRepository : ITransactionRepository
{
    private readonly BillingDbContext _context;

    public TransactionRepository(BillingDbContext context)
    {
        _context = context;
    }

    public async Task<Transaction?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Transactions.Find(t => t.Id == id).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<Transaction>> GetByBillingAccountIdAsync(Guid billingAccountId, CancellationToken cancellationToken = default)
    {
        return await _context.Transactions.Find(t => t.BillingAccountId == billingAccountId).ToListAsync(cancellationToken);
    }

    public async Task<List<Transaction>> GetByInvoiceIdAsync(Guid invoiceId, CancellationToken cancellationToken = default)
    {
        return await _context.Transactions.Find(t => t.InvoiceId == invoiceId).ToListAsync(cancellationToken);
    }

    public async Task<List<Transaction>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Transactions.Find(_ => true).ToListAsync(cancellationToken);
    }

    public async Task<Transaction> AddAsync(Transaction transaction, CancellationToken cancellationToken = default)
    {
        await _context.Transactions.InsertOneAsync(transaction, cancellationToken: cancellationToken);
        return transaction;
    }
}

public class CreditNoteRepository : ICreditNoteRepository
{
    private readonly BillingDbContext _context;

    public CreditNoteRepository(BillingDbContext context)
    {
        _context = context;
    }

    public async Task<CreditNote?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.CreditNotes.Find(cn => cn.Id == id).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<CreditNote?> GetByNumberAsync(string creditNoteNumber, CancellationToken cancellationToken = default)
    {
        return await _context.CreditNotes.Find(cn => cn.CreditNoteNumber == creditNoteNumber).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<CreditNote>> GetByBillingAccountIdAsync(Guid billingAccountId, CancellationToken cancellationToken = default)
    {
        return await _context.CreditNotes.Find(cn => cn.BillingAccountId == billingAccountId).ToListAsync(cancellationToken);
    }

    public async Task<List<CreditNote>> GetByInvoiceIdAsync(Guid invoiceId, CancellationToken cancellationToken = default)
    {
        return await _context.CreditNotes.Find(cn => cn.InvoiceId == invoiceId).ToListAsync(cancellationToken);
    }

    public async Task<List<CreditNote>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.CreditNotes.Find(_ => true).ToListAsync(cancellationToken);
    }

    public async Task<CreditNote> AddAsync(CreditNote creditNote, CancellationToken cancellationToken = default)
    {
        await _context.CreditNotes.InsertOneAsync(creditNote, cancellationToken: cancellationToken);
        return creditNote;
    }

    public async Task UpdateAsync(CreditNote creditNote, CancellationToken cancellationToken = default)
    {
        await _context.CreditNotes.ReplaceOneAsync(cn => cn.Id == creditNote.Id, creditNote, cancellationToken: cancellationToken);
    }
}
