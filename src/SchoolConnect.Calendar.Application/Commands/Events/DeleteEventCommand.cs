using MediatR;

namespace SchoolConnect.Calendar.Application.Commands.Events;

public record DeleteEventCommand(
    Guid EventId,
    Guid DeletedBy) : IRequest<Unit>;
