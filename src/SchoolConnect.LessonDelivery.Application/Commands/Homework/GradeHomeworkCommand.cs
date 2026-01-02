using MediatR;

namespace SchoolConnect.LessonDelivery.Application.Commands.Homework;

public record GradeHomeworkCommand : IRequest<Unit>
{
    public Guid SubmissionId { get; init; }
    public decimal MarksObtained { get; init; }
    public decimal MaxMarks { get; init; }
    public Guid GradedBy { get; init; }
    public string? Feedback { get; init; }
    public string? AudioFeedbackUrl { get; init; }
}
