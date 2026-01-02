using MediatR;

namespace SchoolConnect.LessonDelivery.Application.Commands.Sessions;

public record StartLessonCommand : IRequest<Guid>
{
    public Guid ScheduledLessonId { get; init; }
    public Guid ClassId { get; init; }
    public Guid TeacherId { get; init; }
    public Guid? LessonPlanId { get; init; }
}
