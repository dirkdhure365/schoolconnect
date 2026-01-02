namespace SchoolConnect.Common.Infrastructure.Messaging.Contracts;

public record StudentEnrolledIntegrationEvent : IntegrationEvent
{
    public Guid StudentId { get; init; }
    public string StudentName { get; init; } = string.Empty;
    public Guid ProgramId { get; init; }
    public string ProgramName { get; init; } = string.Empty;
    public DateTime EnrollmentDate { get; init; }
}
