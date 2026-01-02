namespace SchoolConnect.LessonDelivery.Domain.Exceptions;

public class InvalidLessonStateException : Exception
{
    public InvalidLessonStateException(string message)
        : base(message)
    {
    }
}
