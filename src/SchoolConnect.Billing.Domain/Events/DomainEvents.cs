using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Billing.Domain.Events;

public record BillingAccountCreatedEvent : DomainEvent
{
    public required string EventType { get; init; }
    public required Guid InstituteId { get; init; }
    public required string BillingEmail { get; init; }
}

public record InvoiceCreatedEvent : DomainEvent
{
    public required string EventType { get; init; }
    public required string InvoiceNumber { get; init; }
    public required Guid InstituteId { get; init; }
}

public record InvoiceSentEvent : DomainEvent
{
    public required string EventType { get; init; }
    public required string InvoiceNumber { get; init; }
    public required Guid InstituteId { get; init; }
}

public record InvoicePaidEvent : DomainEvent
{
    public required string EventType { get; init; }
    public required string InvoiceNumber { get; init; }
    public required Guid InstituteId { get; init; }
    public required decimal Amount { get; init; }
}

public record InvoiceOverdueEvent : DomainEvent
{
    public required string EventType { get; init; }
    public required string InvoiceNumber { get; init; }
    public required Guid InstituteId { get; init; }
}

public record InvoiceCancelledEvent : DomainEvent
{
    public required string EventType { get; init; }
    public required string InvoiceNumber { get; init; }
    public required Guid InstituteId { get; init; }
}

public record PaymentInitiatedEvent : DomainEvent
{
    public required string EventType { get; init; }
    public required Guid InvoiceId { get; init; }
    public required decimal Amount { get; init; }
}

public record PaymentCompletedEvent : DomainEvent
{
    public required string EventType { get; init; }
    public required Guid InvoiceId { get; init; }
    public required decimal Amount { get; init; }
    public required string TransactionReference { get; init; }
}

public record PaymentFailedEvent : DomainEvent
{
    public required string EventType { get; init; }
    public required Guid InvoiceId { get; init; }
    public required string Reason { get; init; }
}

public record PaymentRefundedEvent : DomainEvent
{
    public required string EventType { get; init; }
    public required Guid InvoiceId { get; init; }
    public required decimal RefundAmount { get; init; }
}

public record PaymentMethodAddedEvent : DomainEvent
{
    public required string EventType { get; init; }
    public required Guid BillingAccountId { get; init; }
    public required string Type { get; init; }
}

public record PaymentMethodRemovedEvent : DomainEvent
{
    public required string EventType { get; init; }
    public required Guid BillingAccountId { get; init; }
}

public record CreditNoteIssuedEvent : DomainEvent
{
    public required string EventType { get; init; }
    public required string CreditNoteNumber { get; init; }
    public required decimal Amount { get; init; }
}

public record CreditNoteAppliedEvent : DomainEvent
{
    public required string EventType { get; init; }
    public required string CreditNoteNumber { get; init; }
    public required Guid InvoiceId { get; init; }
}
