namespace SchoolConnect.Calendar.Domain.Exceptions;

public class EventNotFoundException : Exception
{
    public EventNotFoundException(Guid eventId)
        : base($"Calendar event with ID '{eventId}' was not found.")
    {
    }
}
