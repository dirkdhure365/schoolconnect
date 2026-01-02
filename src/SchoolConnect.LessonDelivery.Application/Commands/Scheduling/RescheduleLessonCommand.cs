using MediatR;

namespace SchoolConnect.LessonDelivery.Application.Commands.Scheduling;

public record RescheduleLessonCommand : IRequest<Unit>
{
    public Guid Id { get; init; }
    public DateTime NewScheduledStart { get; init; }
    public DateTime NewScheduledEnd { get; init; }
}
