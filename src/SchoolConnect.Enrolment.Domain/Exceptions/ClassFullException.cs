namespace SchoolConnect.Enrolment.Domain.Exceptions;

public class ClassFullException : Exception
{
    public ClassFullException(Guid classId, int capacity)
        : base($"Class with ID '{classId}' has reached its maximum capacity of {capacity} students.")
    {
    }

    public ClassFullException(string className, int capacity)
        : base($"Class '{className}' has reached its maximum capacity of {capacity} students.")
    {
    }
}
