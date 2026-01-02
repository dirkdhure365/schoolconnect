namespace SchoolConnect.Calendar.Domain.Exceptions;

public class SlotNotAvailableException : Exception
{
    public SlotNotAvailableException(string message)
        : base(message)
    {
    }
}
