namespace SchoolConnect.Institution.Domain.Exceptions;

public class CentreNotFoundException : Exception
{
    public CentreNotFoundException(Guid centreId)
        : base($"Centre with ID '{centreId}' was not found.")
    {
    }
}
