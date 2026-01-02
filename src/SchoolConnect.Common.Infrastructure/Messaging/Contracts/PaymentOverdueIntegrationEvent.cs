namespace SchoolConnect.Common.Infrastructure.Messaging.Contracts;

public record PaymentOverdueIntegrationEvent : IntegrationEvent
{
    public Guid InvoiceId { get; init; }
    public Guid StudentId { get; init; }
    public decimal Amount { get; init; }
    public string Currency { get; init; } = "USD";
    public DateTime DueDate { get; init; }
    public int DaysOverdue { get; init; }
    public string InvoiceType { get; init; } = string.Empty;
}
