using SchoolConnect.Enrolment.Domain.Enums;

namespace SchoolConnect.Enrolment.Application.DTOs;

public record AdmissionPeriodDto
{
    public Guid Id { get; init; }
    public Guid InstituteId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public List<Guid> ProgramOfferingIds { get; init; } = [];
    public int AcademicYear { get; init; }
    public DateTime ApplicationStartDate { get; init; }
    public DateTime ApplicationEndDate { get; init; }
    public int? MaxApplications { get; init; }
    public AdmissionPeriodStatus Status { get; init; }
    public List<string> RequiredDocuments { get; init; } = [];
    public decimal? ApplicationFee { get; init; }
    public string? Currency { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
}
