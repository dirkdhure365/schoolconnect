namespace SchoolConnect.Calendar.Domain.Exceptions;

public class TimetableConflictException : Exception
{
    public TimetableConflictException(string message)
        : base(message)
    {
    }
}
