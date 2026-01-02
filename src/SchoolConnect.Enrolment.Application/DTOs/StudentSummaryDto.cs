namespace SchoolConnect.Enrolment.Application.DTOs;

public record StudentSummaryDto
{
    public Guid Id { get; init; }
    public string StudentCode { get; init; } = string.Empty;
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public DateTime DateOfBirth { get; init; }
    public string Status { get; init; } = string.Empty;
}
