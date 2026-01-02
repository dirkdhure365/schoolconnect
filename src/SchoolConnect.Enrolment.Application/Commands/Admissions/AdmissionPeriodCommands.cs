using MediatR;

namespace SchoolConnect.Enrolment.Application.Commands.Admissions;

public record CreateAdmissionPeriodCommand : IRequest<Guid>
{
    public Guid InstituteId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public List<Guid> ProgramOfferingIds { get; init; } = [];
    public int AcademicYear { get; init; }
    public DateTime ApplicationStartDate { get; init; }
    public DateTime ApplicationEndDate { get; init; }
    public int? MaxApplications { get; init; }
    public List<string>? RequiredDocuments { get; init; }
    public decimal? ApplicationFee { get; init; }
    public string? Currency { get; init; }
}

public record OpenAdmissionPeriodCommand : IRequest<Unit>
{
    public Guid Id { get; init; }
}

public record CloseAdmissionPeriodCommand : IRequest<Unit>
{
    public Guid Id { get; init; }
}
