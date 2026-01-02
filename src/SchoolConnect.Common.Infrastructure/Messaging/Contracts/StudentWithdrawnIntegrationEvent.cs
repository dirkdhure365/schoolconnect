namespace SchoolConnect.Common.Infrastructure.Messaging.Contracts;

public record StudentWithdrawnIntegrationEvent : IntegrationEvent
{
    public Guid StudentId { get; init; }
    public string StudentName { get; init; } = string.Empty;
    public Guid ProgramId { get; init; }
    public DateTime WithdrawalDate { get; init; }
    public string Reason { get; init; } = string.Empty;
}
