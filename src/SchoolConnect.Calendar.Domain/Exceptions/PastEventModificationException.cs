namespace SchoolConnect.Calendar.Domain.Exceptions;

public class PastEventModificationException : Exception
{
    public PastEventModificationException(Guid eventId)
        : base($"Cannot modify past event with ID {eventId}")
    {
    }
}
