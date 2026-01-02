using SchoolConnect.Institution.Domain.Enums;

namespace SchoolConnect.Institution.Application.DTOs;

public class CentreDto
{
    public Guid Id { get; set; }
    public Guid InstituteId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public AddressDto Address { get; set; } = null!;
    public ContactInfoDto ContactInfo { get; set; } = null!;
    public GeoLocationDto? Location { get; set; }
    public int Capacity { get; set; }
    public CentreStatus Status { get; set; }
    public WorkingHoursDto? WorkingHours { get; set; }
    public Guid? CentreAdminId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class CentreSummaryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public CentreStatus Status { get; set; }
    public int Capacity { get; set; }
}

public class CentreDashboardDto
{
    public Guid CentreId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int TotalFacilities { get; set; }
    public int TotalResources { get; set; }
    public int TotalStaff { get; set; }
    public CentreStatus Status { get; set; }
}
