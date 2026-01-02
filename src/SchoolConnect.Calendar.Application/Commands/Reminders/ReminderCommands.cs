using MediatR;
using SchoolConnect.Calendar.Domain.Enums;

namespace SchoolConnect.Calendar.Application.Commands.Reminders;

public record CreateReminderCommand(
    Guid EventId,
    Guid UserId,
    int MinutesBefore,
    DateTime EventStartTime,
    ReminderChannel Channel
) : IRequest<Guid>;

public record DeleteReminderCommand(
    Guid ReminderId
) : IRequest<Unit>;
