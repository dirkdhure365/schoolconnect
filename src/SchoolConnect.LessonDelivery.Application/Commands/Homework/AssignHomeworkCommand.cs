using MediatR;

namespace SchoolConnect.LessonDelivery.Application.Commands.Homework;

public record AssignHomeworkCommand : IRequest<Guid>
{
    public Guid ClassId { get; init; }
    public string ClassName { get; init; } = string.Empty;
    public Guid SubjectId { get; init; }
    public string SubjectName { get; init; } = string.Empty;
    public Guid TeacherId { get; init; }
    public string TeacherName { get; init; } = string.Empty;
    public string Title { get; init; } = string.Empty;
    public DateTime DueDate { get; init; }
    public Guid? LessonSessionId { get; init; }
    public string? Description { get; init; }
    public string? Instructions { get; init; }
    public int? MaxMarks { get; init; }
    public bool AllowLateSubmission { get; init; }
}
