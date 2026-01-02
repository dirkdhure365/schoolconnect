using SchoolConnect.Institution.Domain.Enums;

namespace SchoolConnect.Institution.Domain.DTOs;

public record InstituteDto(
    Guid Id,
    string Name,
    string Code,
    string? LogoUrl,
    string? Description,
    InstituteType Type,
    InstituteStatus Status,
    ContactInfoDto ContactInfo,
    AddressDto Address,
    string Country,
    string Timezone,
    int AcademicYearStartMonth,
    InstituteSettingsDto Settings,
    Guid? SubscriptionId,
    DateTime CreatedAt,
    DateTime UpdatedAt
);

public record InstituteSummaryDto(
    Guid Id,
    string Name,
    string Code,
    InstituteType Type,
    InstituteStatus Status,
    string Country
);

public record InstituteSettingsDto(
    string DefaultLanguage,
    string DateFormat,
    string TimeFormat,
    string Currency,
    List<string> EnabledFeatures
);

public record InstituteDashboardDto(
    Guid Id,
    string Name,
    int TotalCentres,
    int ActiveCentres,
    int TotalStaff,
    int ActiveStaff,
    int TotalFacilities,
    int TotalResources
);

public record CentreDto(
    Guid Id,
    Guid InstituteId,
    string Name,
    string Code,
    AddressDto Address,
    ContactInfoDto ContactInfo,
    GeoLocationDto? Location,
    int Capacity,
    CentreStatus Status,
    WorkingHoursDto WorkingHours,
    Guid? CentreAdminId,
    DateTime CreatedAt,
    DateTime UpdatedAt
);

public record CentreSummaryDto(
    Guid Id,
    string Name,
    string Code,
    CentreStatus Status,
    int Capacity
);

public record CentreDashboardDto(
    Guid Id,
    string Name,
    int TotalFacilities,
    int AvailableFacilities,
    int TotalResources,
    int AvailableResources,
    int TotalStaff
);

public record FacilityDto(
    Guid Id,
    Guid CentreId,
    string Name,
    string? Code,
    FacilityType Type,
    int Capacity,
    string? Description,
    List<string> Amenities,
    string? ImageUrl,
    FacilityStatus Status,
    bool IsBookable,
    BookingRulesDto? BookingRules,
    DateTime CreatedAt,
    DateTime UpdatedAt
);

public record FacilityBookingDto(
    Guid Id,
    Guid FacilityId,
    Guid BookedBy,
    string Purpose,
    string? Description,
    DateTime StartTime,
    DateTime EndTime,
    BookingStatus Status,
    string? Notes,
    Guid? ApprovedBy,
    DateTime? ApprovedAt,
    string? CancellationReason,
    DateTime CreatedAt
);

public record FacilityScheduleDto(
    Guid FacilityId,
    string FacilityName,
    List<FacilityBookingDto> Bookings
);

public record ResourceDto(
    Guid Id,
    Guid CentreId,
    string Name,
    ResourceType Type,
    string? SerialNumber,
    string? Description,
    string? ImageUrl,
    ResourceCondition Condition,
    ResourceStatus Status,
    DateTime? AcquisitionDate,
    decimal? Value,
    string? Currency,
    string? Location,
    Dictionary<string, string> Attributes,
    DateTime CreatedAt,
    DateTime UpdatedAt
);

public record ResourceAllocationDto(
    Guid Id,
    Guid ResourceId,
    Guid AllocatedToId,
    string AllocatedToType,
    string AllocatedToName,
    Guid AllocatedBy,
    DateTime StartDate,
    DateTime? EndDate,
    DateTime? ReturnedDate,
    ResourceCondition? ConditionOnReturn,
    string? Notes,
    AllocationStatus Status,
    DateTime CreatedAt
);

public record ResourceInventoryReportDto(
    Guid CentreId,
    string CentreName,
    int TotalResources,
    int AvailableResources,
    int AllocatedResources,
    int UnderRepairResources,
    int LostResources,
    int RetiredResources,
    Dictionary<ResourceType, int> ResourcesByType
);

public record StaffMemberDto(
    Guid Id,
    Guid InstituteId,
    Guid UserId,
    string EmployeeCode,
    string FirstName,
    string LastName,
    string? JobTitle,
    string? Department,
    EmploymentType EmploymentType,
    DateTime JoinDate,
    DateTime? TerminationDate,
    StaffStatus Status,
    List<string> Qualifications,
    List<string> Specializations,
    int? MaxTeachingHoursPerWeek,
    DateTime CreatedAt,
    DateTime UpdatedAt
);

public record StaffSummaryDto(
    Guid Id,
    string EmployeeCode,
    string FirstName,
    string LastName,
    string? JobTitle,
    StaffStatus Status
);

public record TeamDto(
    Guid Id,
    Guid InstituteId,
    Guid? CentreId,
    string Name,
    string? Description,
    TeamType Type,
    Guid? LeaderId,
    DateTime CreatedAt,
    DateTime UpdatedAt
);

public record TeamMemberDto(
    Guid Id,
    Guid TeamId,
    Guid StaffMemberId,
    string StaffMemberName,
    string? Role,
    DateTime JoinedAt,
    DateTime? LeftAt
);

// Value Object DTOs
public record AddressDto(
    string Street,
    string City,
    string State,
    string PostalCode,
    string Country
);

public record ContactInfoDto(
    string Email,
    string Phone,
    string? Website
);

public record WorkingHoursDto(
    TimeOnly StartTime,
    TimeOnly EndTime,
    List<DayOfWeek> WorkingDays
);

public record GeoLocationDto(
    double Latitude,
    double Longitude
);

public record BookingRulesDto(
    int MinDurationMinutes,
    int MaxDurationMinutes,
    int AdvanceBookingDays,
    bool RequiresApproval,
    List<string> AllowedRoles
);
