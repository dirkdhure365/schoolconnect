namespace SchoolConnect.Calendar.Domain.Exceptions;

public class InvalidRecurrenceException : Exception
{
    public InvalidRecurrenceException(string message)
        : base(message)
    {
    }
}
