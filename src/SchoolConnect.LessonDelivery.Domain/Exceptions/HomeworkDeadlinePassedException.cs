namespace SchoolConnect.LessonDelivery.Domain.Exceptions;

public class HomeworkDeadlinePassedException : Exception
{
    public HomeworkDeadlinePassedException(Guid homeworkId)
        : base($"Homework with ID '{homeworkId}' deadline has passed.")
    {
    }
}
