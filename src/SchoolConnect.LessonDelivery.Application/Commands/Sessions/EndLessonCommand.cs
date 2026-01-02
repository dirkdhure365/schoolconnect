using MediatR;

namespace SchoolConnect.LessonDelivery.Application.Commands.Sessions;

public record EndLessonCommand : IRequest<Unit>
{
    public Guid Id { get; init; }
    public string? Notes { get; init; }
    public string? Reflection { get; init; }
}
