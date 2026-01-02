using SchoolConnect.Billing.Domain.Entities;
using SchoolConnect.Billing.Domain.Enums;

namespace SchoolConnect.Billing.Domain.Interfaces;

public interface IBillingAccountRepository
{
    Task<BillingAccount?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<BillingAccount?> GetByInstituteIdAsync(Guid instituteId, CancellationToken cancellationToken = default);
    Task<List<BillingAccount>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<BillingAccount> AddAsync(BillingAccount account, CancellationToken cancellationToken = default);
    Task UpdateAsync(BillingAccount account, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}

public interface IInvoiceRepository
{
    Task<Invoice?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Invoice?> GetByInvoiceNumberAsync(string invoiceNumber, CancellationToken cancellationToken = default);
    Task<List<Invoice>> GetByBillingAccountIdAsync(Guid billingAccountId, CancellationToken cancellationToken = default);
    Task<List<Invoice>> GetByStatusAsync(InvoiceStatus status, CancellationToken cancellationToken = default);
    Task<List<Invoice>> GetOverdueInvoicesAsync(CancellationToken cancellationToken = default);
    Task<List<Invoice>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Invoice> AddAsync(Invoice invoice, CancellationToken cancellationToken = default);
    Task UpdateAsync(Invoice invoice, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}

public interface IPaymentRepository
{
    Task<Payment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Payment>> GetByInvoiceIdAsync(Guid invoiceId, CancellationToken cancellationToken = default);
    Task<List<Payment>> GetByBillingAccountIdAsync(Guid billingAccountId, CancellationToken cancellationToken = default);
    Task<List<Payment>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Payment> AddAsync(Payment payment, CancellationToken cancellationToken = default);
    Task UpdateAsync(Payment payment, CancellationToken cancellationToken = default);
}

public interface IPaymentMethodRepository
{
    Task<PaymentMethod?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<PaymentMethod>> GetByBillingAccountIdAsync(Guid billingAccountId, CancellationToken cancellationToken = default);
    Task<PaymentMethod?> GetDefaultByBillingAccountIdAsync(Guid billingAccountId, CancellationToken cancellationToken = default);
    Task<List<PaymentMethod>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<PaymentMethod> AddAsync(PaymentMethod paymentMethod, CancellationToken cancellationToken = default);
    Task UpdateAsync(PaymentMethod paymentMethod, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}

public interface ITransactionRepository
{
    Task<Transaction?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Transaction>> GetByBillingAccountIdAsync(Guid billingAccountId, CancellationToken cancellationToken = default);
    Task<List<Transaction>> GetByInvoiceIdAsync(Guid invoiceId, CancellationToken cancellationToken = default);
    Task<List<Transaction>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Transaction> AddAsync(Transaction transaction, CancellationToken cancellationToken = default);
}

public interface ICreditNoteRepository
{
    Task<CreditNote?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<CreditNote?> GetByNumberAsync(string creditNoteNumber, CancellationToken cancellationToken = default);
    Task<List<CreditNote>> GetByBillingAccountIdAsync(Guid billingAccountId, CancellationToken cancellationToken = default);
    Task<List<CreditNote>> GetByInvoiceIdAsync(Guid invoiceId, CancellationToken cancellationToken = default);
    Task<List<CreditNote>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<CreditNote> AddAsync(CreditNote creditNote, CancellationToken cancellationToken = default);
    Task UpdateAsync(CreditNote creditNote, CancellationToken cancellationToken = default);
}
