namespace SchoolConnect.Institution.Domain.Exceptions;

public class ResourceNotAvailableException : Exception
{
    public ResourceNotAvailableException(Guid resourceId)
        : base($"Resource with ID '{resourceId}' is not available for allocation.")
    {
    }
}
