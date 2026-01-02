using SchoolConnect.Billing.Domain.Enums;

namespace SchoolConnect.Billing.Application.DTOs;

public record BillingAccountDto(
    Guid Id,
    Guid InstituteId,
    string BillingName,
    string BillingEmail,
    string Street,
    string City,
    string State,
    string PostalCode,
    string Country,
    string? TaxId,
    string? VatNumber,
    Guid? DefaultPaymentMethodId,
    string Currency,
    decimal Balance,
    DateTime CreatedAt,
    DateTime UpdatedAt
);

public record InvoiceDto(
    Guid Id,
    string InvoiceNumber,
    Guid BillingAccountId,
    Guid SubscriptionId,
    Guid InstituteId,
    InvoiceStatus Status,
    DateTime IssueDate,
    DateTime DueDate,
    DateTime? PaidDate,
    List<InvoiceLineItemDto> LineItems,
    decimal Subtotal,
    decimal TaxAmount,
    decimal TaxRate,
    decimal DiscountAmount,
    decimal Total,
    decimal AmountPaid,
    decimal AmountDue,
    string Currency,
    string? Notes,
    DateTime CreatedAt,
    DateTime UpdatedAt
);

public record InvoiceLineItemDto(
    Guid Id,
    string Description,
    int Quantity,
    decimal UnitPrice,
    decimal Amount,
    DateTime? PeriodStart,
    DateTime? PeriodEnd,
    decimal DiscountPercent,
    decimal DiscountAmount
);

public record PaymentDto(
    Guid Id,
    Guid InvoiceId,
    Guid BillingAccountId,
    Guid? PaymentMethodId,
    decimal Amount,
    string Currency,
    PaymentStatus Status,
    PaymentMethodEnum PaymentMethod,
    string? TransactionReference,
    DateTime? ProcessedAt,
    DateTime? FailedAt,
    string? FailureReason,
    DateTime CreatedAt,
    DateTime UpdatedAt
);

public record PaymentMethodDto(
    Guid Id,
    Guid BillingAccountId,
    PaymentMethodType Type,
    bool IsDefault,
    bool IsActive,
    string? CardLast4,
    string? CardBrand,
    int? CardExpiryMonth,
    int? CardExpiryYear,
    string? BankName,
    string? BankAccountLast4,
    string? MobileMoneyProvider,
    string? MobileMoneyLast4,
    DateTime CreatedAt,
    DateTime UpdatedAt
);

public record TransactionDto(
    Guid Id,
    Guid BillingAccountId,
    Guid? InvoiceId,
    Guid? PaymentId,
    TransactionType Type,
    decimal Amount,
    string Currency,
    decimal BalanceBefore,
    decimal BalanceAfter,
    string Description,
    string? Reference,
    DateTime CreatedAt
);

public record CreditNoteDto(
    Guid Id,
    Guid BillingAccountId,
    Guid? InvoiceId,
    string CreditNoteNumber,
    decimal Amount,
    string Currency,
    string Reason,
    CreditNoteStatus Status,
    DateTime IssuedAt,
    DateTime? AppliedAt,
    DateTime? ExpiresAt,
    Guid? AppliedToInvoiceId,
    DateTime CreatedAt,
    DateTime UpdatedAt
);
