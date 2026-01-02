namespace SchoolConnect.Enrolment.Domain.Exceptions;

public class DuplicateEnrolmentException : Exception
{
    public DuplicateEnrolmentException(Guid studentId, Guid streamId)
        : base($"Student with ID '{studentId}' is already enrolled in stream '{streamId}'.")
    {
    }

    public DuplicateEnrolmentException(Guid studentId, Guid classId, string message)
        : base(message)
    {
    }
}
