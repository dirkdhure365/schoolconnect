namespace SchoolConnect.LessonDelivery.Domain.Exceptions;

public class LessonPlanNotFoundException : Exception
{
    public LessonPlanNotFoundException(Guid lessonPlanId)
        : base($"Lesson plan with ID '{lessonPlanId}' was not found.")
    {
    }
}
