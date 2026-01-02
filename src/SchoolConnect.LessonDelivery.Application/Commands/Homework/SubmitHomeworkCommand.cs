using MediatR;

namespace SchoolConnect.LessonDelivery.Application.Commands.Homework;

public record SubmitHomeworkCommand : IRequest<Guid>
{
    public Guid HomeworkId { get; init; }
    public Guid StudentId { get; init; }
    public string StudentName { get; init; } = string.Empty;
    public string StudentCode { get; init; } = string.Empty;
    public string? Content { get; init; }
    public List<string> AttachmentUrls { get; init; } = [];
    public int AttemptNumber { get; init; } = 1;
}
