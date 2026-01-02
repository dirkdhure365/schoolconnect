using MediatR;

namespace SchoolConnect.LessonDelivery.Application.Commands.LessonPlans;

public record CreateLessonPlanCommand : IRequest<Guid>
{
    public Guid ClassId { get; init; }
    public string ClassName { get; init; } = string.Empty;
    public Guid SubjectId { get; init; }
    public string SubjectName { get; init; } = string.Empty;
    public Guid TeacherId { get; init; }
    public string TeacherName { get; init; } = string.Empty;
    public string Title { get; init; } = string.Empty;
    public string? Description { get; init; }
    public int DurationMinutes { get; init; }
    public int? GradeLevel { get; init; }
    public int? TermNumber { get; init; }
    public int? WeekNumber { get; init; }
}
