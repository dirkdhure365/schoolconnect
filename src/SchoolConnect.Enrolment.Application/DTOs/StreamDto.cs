using SchoolConnect.Enrolment.Domain.Enums;

namespace SchoolConnect.Enrolment.Application.DTOs;

public record StreamDto
{
    public Guid Id { get; init; }
    public Guid InstituteId { get; init; }
    public Guid ProgramOfferingId { get; init; }
    public string ProgramOfferingName { get; init; } = string.Empty;
    public int Year { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public StreamStatus Status { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime ExpectedEndDate { get; init; }
}
