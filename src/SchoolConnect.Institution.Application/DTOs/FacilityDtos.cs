using SchoolConnect.Institution.Domain.Enums;

namespace SchoolConnect.Institution.Application.DTOs;

public class FacilityDto
{
    public Guid Id { get; set; }
    public Guid CentreId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Code { get; set; }
    public FacilityType Type { get; set; }
    public int Capacity { get; set; }
    public string? Description { get; set; }
    public List<string> Amenities { get; set; } = [];
    public string? ImageUrl { get; set; }
    public FacilityStatus Status { get; set; }
    public bool IsBookable { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class FacilityBookingDto
{
    public Guid Id { get; set; }
    public Guid FacilityId { get; set; }
    public Guid BookedBy { get; set; }
    public string Purpose { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public BookingStatus Status { get; set; }
    public string? Notes { get; set; }
    public Guid? ApprovedBy { get; set; }
    public DateTime? ApprovedAt { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class FacilityScheduleDto
{
    public Guid FacilityId { get; set; }
    public string FacilityName { get; set; } = string.Empty;
    public List<FacilityBookingDto> Bookings { get; set; } = [];
}
