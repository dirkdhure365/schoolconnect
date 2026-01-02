namespace SchoolConnect.Enrolment.Domain.Exceptions;

public class InvalidPromotionException : Exception
{
    public InvalidPromotionException(string message)
        : base(message)
    {
    }

    public InvalidPromotionException(Guid studentId, string reason)
        : base($"Cannot promote student with ID '{studentId}': {reason}")
    {
    }
}
