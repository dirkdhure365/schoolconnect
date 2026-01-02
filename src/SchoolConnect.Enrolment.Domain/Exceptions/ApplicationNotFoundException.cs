namespace SchoolConnect.Enrolment.Domain.Exceptions;

public class ApplicationNotFoundException : Exception
{
    public ApplicationNotFoundException(Guid applicationId)
        : base($"Application with ID '{applicationId}' was not found.")
    {
    }

    public ApplicationNotFoundException(string applicationNumber)
        : base($"Application with number '{applicationNumber}' was not found.")
    {
    }
}
