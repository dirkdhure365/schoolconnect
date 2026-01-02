using MediatR;
using SchoolConnect.Calendar.Application.DTOs;

namespace SchoolConnect.Calendar.Application.Queries.Events;

public class GetEventById2Query : IRequest<CalendarEventDto?>
{
    public Guid EventId { get; }

    public GetEventById2Query(Guid eventId)
    {
        EventId = eventId;
    }
}
