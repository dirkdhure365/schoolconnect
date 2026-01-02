namespace SchoolConnect.Institution.Domain.Exceptions;

public class StaffAlreadyAssignedException : Exception
{
    public StaffAlreadyAssignedException(Guid staffId, Guid centreId)
        : base($"Staff member '{staffId}' is already assigned to centre '{centreId}'.")
    {
    }
}
