using MediatR;

namespace SchoolConnect.Calendar.Application.Commands.Events;

public record CancelEventCommand(
    Guid EventId,
    string Reason,
    Guid CancelledBy) : IRequest<Unit>;
