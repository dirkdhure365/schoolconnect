namespace SchoolConnect.Institution.Domain.Entities;

public class BookingRules
{
    public int MinDurationMinutes { get; set; } = 30;
    public int MaxDurationMinutes { get; set; } = 480;
    public int AdvanceBookingDays { get; set; } = 30;
    public bool RequiresApproval { get; set; } = false;
    public List<string> AllowedRoles { get; set; } = new();
}
