using MediatR;

namespace SchoolConnect.Calendar.Application.Commands.Events;

public record DeleteEvent2Command(Guid EventId, Guid DeletedBy) : IRequest<Unit>;
