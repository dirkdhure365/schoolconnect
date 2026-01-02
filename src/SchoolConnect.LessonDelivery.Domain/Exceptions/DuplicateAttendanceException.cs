namespace SchoolConnect.LessonDelivery.Domain.Exceptions;

public class DuplicateAttendanceException : Exception
{
    public DuplicateAttendanceException(Guid studentId, Guid sessionId)
        : base($"Attendance for student '{studentId}' in session '{sessionId}' has already been recorded.")
    {
    }
}
