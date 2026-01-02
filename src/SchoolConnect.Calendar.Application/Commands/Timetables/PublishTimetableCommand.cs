using MediatR;

namespace SchoolConnect.Calendar.Application.Commands.Timetables;

public record PublishTimetableCommand(
    Guid TimetableId,
    Guid PublishedBy) : IRequest<Unit>;
