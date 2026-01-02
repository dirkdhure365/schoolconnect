using SchoolConnect.Enrolment.Domain.Enums;

namespace SchoolConnect.Enrolment.Application.DTOs;

public record CohortDto
{
    public Guid Id { get; init; }
    public Guid StreamId { get; init; }
    public Guid CentreId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public int Capacity { get; init; }
    public int CurrentCount { get; init; }
    public Guid? CohortAdvisorId { get; init; }
    public CohortStatus Status { get; init; }
}
