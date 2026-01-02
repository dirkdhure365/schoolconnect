namespace SchoolConnect.Institution.Domain.Exceptions;

public class InstituteNotFoundException : Exception
{
    public InstituteNotFoundException(Guid instituteId)
        : base($"Institute with ID '{instituteId}' was not found.")
    {
    }
}
