using MediatR;
using SchoolConnect.Calendar.Domain.Enums;

namespace SchoolConnect.Calendar.Application.Commands.Events;

public record RsvpEvent2Command(Guid EventId, Guid UserId, RsvpStatus Status, string? Notes = null)
    : IRequest<Unit>;
