namespace SchoolConnect.Institution.Domain.Exceptions;

public class FacilityNotAvailableException : Exception
{
    public FacilityNotAvailableException(Guid facilityId, DateTime startTime, DateTime endTime)
        : base($"Facility with ID '{facilityId}' is not available from {startTime} to {endTime}.")
    {
    }
}
