namespace SchoolConnect.LessonDelivery.Domain.Exceptions;

public class LessonAlreadyStartedException : Exception
{
    public LessonAlreadyStartedException(Guid lessonId)
        : base($"Lesson with ID '{lessonId}' has already been started.")
    {
    }
}
