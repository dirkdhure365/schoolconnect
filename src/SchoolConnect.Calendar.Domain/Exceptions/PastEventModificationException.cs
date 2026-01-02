namespace SchoolConnect.Calendar.Domain.Exceptions;

public class PastEventModificationException : Exception
{
    public PastEventModificationException()
        : base("Cannot modify events that have already occurred.")
    {
    }
    
    public PastEventModificationException(string message)
        : base(message)
    {
    }
}
