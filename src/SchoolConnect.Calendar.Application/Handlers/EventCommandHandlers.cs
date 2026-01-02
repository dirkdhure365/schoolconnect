using MediatR;
using SchoolConnect.Calendar.Application.Commands.Events;
using SchoolConnect.Calendar.Domain.Entities;
using SchoolConnect.Calendar.Domain.Interfaces;
using SchoolConnect.Calendar.Domain.Exceptions;

namespace SchoolConnect.Calendar.Application.Handlers;

public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Guid>
{
    private readonly IEventRepository _eventRepository;

    public CreateEventCommandHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<Guid> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        var calendarEvent = CalendarEvent.Create(
            request.InstituteId,
            request.Title,
            request.StartTime,
            request.EndTime,
            request.CreatedBy,
            request.CreatedByName,
            request.CentreId,
            request.ClassId,
            request.Description,
            request.Location,
            request.IsAllDay,
            request.Timezone,
            request.Type,
            request.Visibility,
            request.RsvpRequired
        );

        await _eventRepository.AddAsync(calendarEvent, cancellationToken);
        return calendarEvent.Id;
    }
}

public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, Unit>
{
    private readonly IEventRepository _eventRepository;

    public UpdateEventCommandHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<Unit> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
    {
        var calendarEvent = await _eventRepository.GetByIdAsync(request.EventId, cancellationToken)
            ?? throw new EventNotFoundException(request.EventId);

        calendarEvent.Update(
            request.Title,
            request.Description,
            request.Location,
            request.StartTime,
            request.EndTime,
            request.IsAllDay,
            request.Type,
            request.Visibility
        );

        await _eventRepository.UpdateAsync(calendarEvent, cancellationToken);
        return Unit.Value;
    }
}

public class CancelEventCommandHandler : IRequestHandler<CancelEventCommand, Unit>
{
    private readonly IEventRepository _eventRepository;

    public CancelEventCommandHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<Unit> Handle(CancelEventCommand request, CancellationToken cancellationToken)
    {
        var calendarEvent = await _eventRepository.GetByIdAsync(request.EventId, cancellationToken)
            ?? throw new EventNotFoundException(request.EventId);

        calendarEvent.Cancel(request.CancellationReason);
        await _eventRepository.UpdateAsync(calendarEvent, cancellationToken);
        return Unit.Value;
    }
}

public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand, Unit>
{
    private readonly IEventRepository _eventRepository;

    public DeleteEventCommandHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<Unit> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
    {
        await _eventRepository.DeleteAsync(request.EventId, cancellationToken);
        return Unit.Value;
    }
}

public class AddAttendeeCommandHandler : IRequestHandler<AddAttendeeCommand, Guid>
{
    private readonly IEventRepository _eventRepository;

    public AddAttendeeCommandHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<Guid> Handle(AddAttendeeCommand request, CancellationToken cancellationToken)
    {
        var calendarEvent = await _eventRepository.GetByIdAsync(request.EventId, cancellationToken)
            ?? throw new EventNotFoundException(request.EventId);

        calendarEvent.AddAttendee();
        await _eventRepository.UpdateAsync(calendarEvent, cancellationToken);
        
        return Guid.NewGuid(); // In a real implementation, this would return the created attendee ID
    }
}
