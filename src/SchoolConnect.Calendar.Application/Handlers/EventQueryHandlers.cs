using MediatR;
using AutoMapper;
using SchoolConnect.Calendar.Application.Queries.Events;
using SchoolConnect.Calendar.Application.DTOs;
using SchoolConnect.Calendar.Domain.Interfaces;

namespace SchoolConnect.Calendar.Application.Handlers;

public class GetEventByIdQueryHandler : IRequestHandler<GetEventByIdQuery, CalendarEventDto?>
{
    private readonly IEventRepository _eventRepository;
    private readonly IMapper _mapper;

    public GetEventByIdQueryHandler(IEventRepository eventRepository, IMapper mapper)
    {
        _eventRepository = eventRepository;
        _mapper = mapper;
    }

    public async Task<CalendarEventDto?> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
    {
        var calendarEvent = await _eventRepository.GetByIdAsync(request.EventId, cancellationToken);
        return calendarEvent == null ? null : _mapper.Map<CalendarEventDto>(calendarEvent);
    }
}

public class GetEventsQueryHandler : IRequestHandler<GetEventsQuery, IEnumerable<CalendarEventDto>>
{
    private readonly IEventRepository _eventRepository;
    private readonly IMapper _mapper;

    public GetEventsQueryHandler(IEventRepository eventRepository, IMapper mapper)
    {
        _eventRepository = eventRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CalendarEventDto>> Handle(GetEventsQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Domain.Entities.CalendarEvent> events;

        if (request.CentreId.HasValue)
            events = await _eventRepository.GetByCentreIdAsync(request.CentreId.Value, cancellationToken);
        else if (request.InstituteId.HasValue)
            events = await _eventRepository.GetByInstituteIdAsync(request.InstituteId.Value, cancellationToken);
        else
            events = new List<Domain.Entities.CalendarEvent>();

        return _mapper.Map<IEnumerable<CalendarEventDto>>(events);
    }
}

public class GetEventsByDateRangeQueryHandler : IRequestHandler<GetEventsByDateRangeQuery, IEnumerable<CalendarEventDto>>
{
    private readonly IEventRepository _eventRepository;
    private readonly IMapper _mapper;

    public GetEventsByDateRangeQueryHandler(IEventRepository eventRepository, IMapper mapper)
    {
        _eventRepository = eventRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CalendarEventDto>> Handle(GetEventsByDateRangeQuery request, CancellationToken cancellationToken)
    {
        var events = await _eventRepository.GetByDateRangeAsync(
            request.StartDate, 
            request.EndDate, 
            request.InstituteId, 
            cancellationToken);
        
        return _mapper.Map<IEnumerable<CalendarEventDto>>(events);
    }
}

public class GetUpcomingEventsQueryHandler : IRequestHandler<GetUpcomingEventsQuery, IEnumerable<CalendarEventDto>>
{
    private readonly IEventRepository _eventRepository;
    private readonly IMapper _mapper;

    public GetUpcomingEventsQueryHandler(IEventRepository eventRepository, IMapper mapper)
    {
        _eventRepository = eventRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CalendarEventDto>> Handle(GetUpcomingEventsQuery request, CancellationToken cancellationToken)
    {
        var events = await _eventRepository.GetUpcomingEventsAsync(request.UserId, request.Count, cancellationToken);
        return _mapper.Map<IEnumerable<CalendarEventDto>>(events);
    }
}
