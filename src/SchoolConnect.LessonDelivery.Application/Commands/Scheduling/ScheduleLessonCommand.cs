using MediatR;

namespace SchoolConnect.LessonDelivery.Application.Commands.Scheduling;

public record ScheduleLessonCommand : IRequest<Guid>
{
    public Guid ClassId { get; init; }
    public string ClassName { get; init; } = string.Empty;
    public Guid SubjectId { get; init; }
    public string SubjectName { get; init; } = string.Empty;
    public Guid TeacherId { get; init; }
    public string TeacherName { get; init; } = string.Empty;
    public DateTime ScheduledStart { get; init; }
    public DateTime ScheduledEnd { get; init; }
    public Guid? LessonPlanId { get; init; }
    public Guid? FacilityId { get; init; }
    public string? FacilityName { get; init; }
    public string? Title { get; init; }
}
