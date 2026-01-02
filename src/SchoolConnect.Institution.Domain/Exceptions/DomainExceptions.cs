namespace SchoolConnect.Institution.Domain.Exceptions;

public class InstituteNotFoundException : Exception
{
    public InstituteNotFoundException(Guid instituteId)
        : base($"Institute with ID '{instituteId}' was not found.")
    {
    }
}

public class CentreNotFoundException : Exception
{
    public CentreNotFoundException(Guid centreId)
        : base($"Centre with ID '{centreId}' was not found.")
    {
    }
}

public class FacilityNotAvailableException : Exception
{
    public FacilityNotAvailableException(Guid facilityId)
        : base($"Facility with ID '{facilityId}' is not available for booking.")
    {
    }
}

public class ResourceNotAvailableException : Exception
{
    public ResourceNotAvailableException(Guid resourceId)
        : base($"Resource with ID '{resourceId}' is not available for allocation.")
    {
    }
}

public class BookingConflictException : Exception
{
    public BookingConflictException(Guid facilityId, DateTime startTime, DateTime endTime)
        : base($"Facility with ID '{facilityId}' is already booked between {startTime} and {endTime}.")
    {
    }
}

public class StaffAlreadyAssignedException : Exception
{
    public StaffAlreadyAssignedException(Guid staffId, Guid centreId)
        : base($"Staff member with ID '{staffId}' is already assigned to centre '{centreId}'.")
    {
    }
}
