namespace SchoolConnect.Common.Infrastructure.Messaging.Contracts;

public record AssessmentGradedIntegrationEvent : IntegrationEvent
{
    public Guid AssessmentId { get; init; }
    public Guid StudentId { get; init; }
    public Guid SubjectId { get; init; }
    public string AssessmentType { get; init; } = string.Empty;
    public decimal Score { get; init; }
    public decimal MaxScore { get; init; }
    public string Grade { get; init; } = string.Empty;
    public DateTime GradedDate { get; init; }
}
