namespace SchoolConnect.Communication.Domain.Exceptions;

public class AnnouncementNotFoundException : Exception
{
    public AnnouncementNotFoundException(Guid announcementId)
        : base($"Announcement with ID '{announcementId}' was not found.")
    {
    }
}
