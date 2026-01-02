using MediatR;

namespace SchoolConnect.LessonDelivery.Application.Commands.Scheduling;

public record CancelLessonCommand : IRequest<Unit>
{
    public Guid Id { get; init; }
    public string? Reason { get; init; }
}
