using MediatR;

namespace SchoolConnect.Calendar.Application.Commands.Events;

public record CancelEvent2Command(Guid EventId, string Reason, Guid CancelledBy) : IRequest<Unit>;
