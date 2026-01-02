using SchoolConnect.Enrolment.Domain.Enums;

namespace SchoolConnect.Enrolment.Application.DTOs;

public record ClassDto
{
    public Guid Id { get; init; }
    public Guid CohortId { get; init; }
    public Guid SubjectId { get; init; }
    public string SubjectName { get; init; } = string.Empty;
    public string SubjectCode { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public Guid? TeacherId { get; init; }
    public string? TeacherName { get; init; }
    public int Capacity { get; init; }
    public int CurrentCount { get; init; }
    public ClassStatus Status { get; init; }
}
