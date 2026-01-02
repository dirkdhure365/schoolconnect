namespace SchoolConnect.Institution.Domain.Exceptions;

public class BookingConflictException : Exception
{
    public BookingConflictException(Guid facilityId, DateTime startTime, DateTime endTime)
        : base($"Booking conflict for facility '{facilityId}' from {startTime} to {endTime}.")
    {
    }
}
