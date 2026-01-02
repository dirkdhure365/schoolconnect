namespace SchoolConnect.Enrolment.Domain.Exceptions;

public class StudentNotFoundException : Exception
{
    public StudentNotFoundException(Guid studentId)
        : base($"Student with ID '{studentId}' was not found.")
    {
    }

    public StudentNotFoundException(string studentCode)
        : base($"Student with code '{studentCode}' was not found.")
    {
    }
}
