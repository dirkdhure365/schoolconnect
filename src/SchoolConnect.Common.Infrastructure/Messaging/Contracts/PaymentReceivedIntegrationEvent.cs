namespace SchoolConnect.Common.Infrastructure.Messaging.Contracts;

public record PaymentReceivedIntegrationEvent : IntegrationEvent
{
    public Guid PaymentId { get; init; }
    public Guid StudentId { get; init; }
    public decimal Amount { get; init; }
    public string Currency { get; init; } = "USD";
    public string PaymentMethod { get; init; } = string.Empty;
    public string PaymentType { get; init; } = string.Empty; // Tuition, Fees, etc.
    public DateTime PaymentDate { get; init; }
    public string? Reference { get; init; }
}
