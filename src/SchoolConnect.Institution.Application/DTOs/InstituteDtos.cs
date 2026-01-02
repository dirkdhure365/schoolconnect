using SchoolConnect.Institution.Domain.Enums;

namespace SchoolConnect.Institution.Application.DTOs;

public class InstituteDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string? LogoUrl { get; set; }
    public string? Description { get; set; }
    public InstituteType Type { get; set; }
    public InstituteStatus Status { get; set; }
    public ContactInfoDto ContactInfo { get; set; } = null!;
    public AddressDto Address { get; set; } = null!;
    public string Country { get; set; } = string.Empty;
    public string Timezone { get; set; } = string.Empty;
    public int AcademicYearStartMonth { get; set; }
    public InstituteSettingsDto Settings { get; set; } = null!;
    public Guid? SubscriptionId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class InstituteSummaryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string? LogoUrl { get; set; }
    public InstituteType Type { get; set; }
    public InstituteStatus Status { get; set; }
    public string Country { get; set; } = string.Empty;
}

public class InstituteSettingsDto
{
    public string DefaultLanguage { get; set; } = "en";
    public string DateFormat { get; set; } = "yyyy-MM-dd";
    public string TimeFormat { get; set; } = "HH:mm";
    public string Currency { get; set; } = "USD";
    public List<string> EnabledFeatures { get; set; } = [];
}

public class InstituteDashboardDto
{
    public Guid InstituteId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int TotalCentres { get; set; }
    public int TotalStaff { get; set; }
    public int TotalTeams { get; set; }
    public InstituteStatus Status { get; set; }
}

public class AddressDto
{
    public string Street { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
}

public class ContactInfoDto
{
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string? Website { get; set; }
}

public class GeoLocationDto
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}

public class WorkingHoursDto
{
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public List<DayOfWeek> WorkingDays { get; set; } = [];
}
