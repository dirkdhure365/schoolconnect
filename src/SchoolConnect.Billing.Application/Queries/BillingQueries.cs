using MediatR;
using SchoolConnect.Billing.Application.DTOs;
using SchoolConnect.Billing.Domain.Enums;

namespace SchoolConnect.Billing.Application.Queries;

// Billing Account Queries
public record GetBillingAccountByIdQuery(Guid Id) : IRequest<BillingAccountDto?>;

public record GetBillingAccountByInstituteQuery(Guid InstituteId) : IRequest<BillingAccountDto?>;

// Invoice Queries
public record GetInvoiceByIdQuery(Guid Id) : IRequest<InvoiceDto?>;

public record GetInvoicesByBillingAccountQuery(Guid BillingAccountId) : IRequest<IEnumerable<InvoiceDto>>;

public record GetInvoicesByStatusQuery(InvoiceStatus Status) : IRequest<IEnumerable<InvoiceDto>>;

public record GetOverdueInvoicesQuery() : IRequest<IEnumerable<InvoiceDto>>;

// Payment Queries
public record GetPaymentByIdQuery(Guid Id) : IRequest<PaymentDto?>;

public record GetPaymentsByInvoiceQuery(Guid InvoiceId) : IRequest<IEnumerable<PaymentDto>>;

// Payment Method Queries
public record GetPaymentMethodsQuery(Guid BillingAccountId) : IRequest<IEnumerable<PaymentMethodDto>>;

// Transaction Queries
public record GetTransactionsQuery(Guid BillingAccountId) : IRequest<IEnumerable<TransactionDto>>;

// Credit Note Queries
public record GetCreditNotesQuery(Guid BillingAccountId) : IRequest<IEnumerable<CreditNoteDto>>;
