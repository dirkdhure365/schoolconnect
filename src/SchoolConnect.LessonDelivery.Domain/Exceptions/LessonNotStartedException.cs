namespace SchoolConnect.LessonDelivery.Domain.Exceptions;

public class LessonNotStartedException : Exception
{
    public LessonNotStartedException(Guid lessonId)
        : base($"Lesson with ID '{lessonId}' has not been started yet.")
    {
    }
}
