namespace SchoolConnect.Calendar.Domain.Exceptions;

public class TimetableNotFoundException : Exception
{
    public TimetableNotFoundException(Guid timetableId)
        : base($"Timetable with ID '{timetableId}' was not found.")
    {
    }
}
