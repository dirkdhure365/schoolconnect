using MediatR;
using SchoolConnect.Billing.Application.DTOs;
using SchoolConnect.Billing.Domain.Enums;

namespace SchoolConnect.Billing.Application.Commands;

// Billing Account Commands
public record CreateBillingAccountCommand(
    Guid InstituteId,
    string BillingName,
    string BillingEmail,
    string Street,
    string City,
    string State,
    string PostalCode,
    string Country,
    string Currency,
    string? TaxId = null,
    string? VatNumber = null
) : IRequest<BillingAccountDto>;

public record UpdateBillingAccountCommand(
    Guid Id,
    string BillingName,
    string BillingEmail,
    string Street,
    string City,
    string State,
    string PostalCode,
    string Country,
    string? TaxId,
    string? VatNumber
) : IRequest<BillingAccountDto>;

// Invoice Commands
public record CreateInvoiceCommand(
    Guid BillingAccountId,
    Guid SubscriptionId,
    Guid InstituteId,
    DateTime IssueDate,
    DateTime DueDate,
    string Currency,
    decimal TaxRate = 0,
    string? Notes = null
) : IRequest<InvoiceDto>;

public record SendInvoiceCommand(Guid Id) : IRequest<InvoiceDto>;

public record MarkInvoicePaidCommand(
    Guid Id,
    decimal Amount,
    DateTime PaidDate
) : IRequest<InvoiceDto>;

public record CancelInvoiceCommand(Guid Id) : IRequest<InvoiceDto>;

// Payment Commands
public record ProcessPaymentCommand(
    Guid InvoiceId,
    Guid BillingAccountId,
    decimal Amount,
    string Currency,
    PaymentMethodEnum PaymentMethod,
    Guid? PaymentMethodId = null
) : IRequest<PaymentDto>;

public record RefundPaymentCommand(
    Guid Id,
    decimal Amount
) : IRequest<PaymentDto>;

// Payment Method Commands
public record AddPaymentMethodCommand(
    Guid BillingAccountId,
    PaymentMethodType Type,
    string? CardLast4 = null,
    string? CardBrand = null,
    int? CardExpiryMonth = null,
    int? CardExpiryYear = null,
    string? BankName = null,
    string? BankAccountLast4 = null,
    string? MobileMoneyProvider = null,
    string? MobileMoneyLast4 = null,
    string? ExternalId = null,
    bool IsDefault = false
) : IRequest<PaymentMethodDto>;

public record RemovePaymentMethodCommand(Guid Id) : IRequest<bool>;

public record SetDefaultPaymentMethodCommand(
    Guid Id,
    Guid BillingAccountId
) : IRequest<PaymentMethodDto>;

// Transaction Commands
public record RecordTransactionCommand(
    Guid BillingAccountId,
    TransactionType Type,
    decimal Amount,
    string Currency,
    decimal BalanceBefore,
    string Description,
    Guid? InvoiceId = null,
    Guid? PaymentId = null,
    string? Reference = null,
    string? CreatedBy = null
) : IRequest<TransactionDto>;

// Credit Note Commands
public record IssueCreditNoteCommand(
    Guid BillingAccountId,
    decimal Amount,
    string Currency,
    string Reason,
    Guid? InvoiceId = null,
    DateTime? ExpiresAt = null,
    string? CreatedBy = null
) : IRequest<CreditNoteDto>;

public record ApplyCreditNoteCommand(
    Guid Id,
    Guid InvoiceId
) : IRequest<CreditNoteDto>;
