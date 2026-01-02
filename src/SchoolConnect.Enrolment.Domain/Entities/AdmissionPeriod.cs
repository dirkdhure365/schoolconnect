using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.Enrolment.Domain.Enums;
using SchoolConnect.Enrolment.Domain.Events;

namespace SchoolConnect.Enrolment.Domain.Entities;

public class AdmissionPeriod : AggregateRoot
{
    public Guid InstituteId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public List<Guid> ProgramOfferingIds { get; private set; } = [];
    public int AcademicYear { get; private set; }
    public DateTime ApplicationStartDate { get; private set; }
    public DateTime ApplicationEndDate { get; private set; }
    public int? MaxApplications { get; private set; }
    public AdmissionPeriodStatus Status { get; private set; }
    public List<string> RequiredDocuments { get; private set; } = [];
    public decimal? ApplicationFee { get; private set; }
    public string? Currency { get; private set; }

    private AdmissionPeriod() { }

    public static AdmissionPeriod Create(
        Guid instituteId,
        string name,
        int academicYear,
        DateTime applicationStartDate,
        DateTime applicationEndDate,
        List<Guid> programOfferingIds,
        string? description = null,
        int? maxApplications = null,
        List<string>? requiredDocuments = null,
        decimal? applicationFee = null,
        string? currency = null)
    {
        var period = new AdmissionPeriod
        {
            InstituteId = instituteId,
            Name = name,
            Description = description,
            ProgramOfferingIds = programOfferingIds ?? [],
            AcademicYear = academicYear,
            ApplicationStartDate = applicationStartDate,
            ApplicationEndDate = applicationEndDate,
            MaxApplications = maxApplications,
            Status = AdmissionPeriodStatus.Draft,
            RequiredDocuments = requiredDocuments ?? [],
            ApplicationFee = applicationFee,
            Currency = currency
        };

        period.AddDomainEvent(new AdmissionPeriodCreatedEvent
        {
            AggregateId = period.Id,
            AggregateType = nameof(AdmissionPeriod),
            InstituteId = instituteId,
            Name = name,
            AcademicYear = academicYear,
            ApplicationStartDate = applicationStartDate,
            ApplicationEndDate = applicationEndDate
        });

        return period;
    }

    public void Update(
        string name,
        DateTime applicationStartDate,
        DateTime applicationEndDate,
        string? description = null,
        int? maxApplications = null,
        List<string>? requiredDocuments = null,
        decimal? applicationFee = null,
        string? currency = null)
    {
        Name = name;
        Description = description;
        ApplicationStartDate = applicationStartDate;
        ApplicationEndDate = applicationEndDate;
        MaxApplications = maxApplications;
        if (requiredDocuments != null) RequiredDocuments = requiredDocuments;
        ApplicationFee = applicationFee;
        Currency = currency;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Open()
    {
        Status = AdmissionPeriodStatus.Open;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new AdmissionPeriodOpenedEvent
        {
            AggregateId = Id,
            AggregateType = nameof(AdmissionPeriod),
            InstituteId = InstituteId,
            Name = Name
        });
    }

    public void Close()
    {
        Status = AdmissionPeriodStatus.Closed;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new AdmissionPeriodClosedEvent
        {
            AggregateId = Id,
            AggregateType = nameof(AdmissionPeriod),
            InstituteId = InstituteId,
            Name = Name
        });
    }

    public void Archive()
    {
        Status = AdmissionPeriodStatus.Archived;
        UpdatedAt = DateTime.UtcNow;
    }

    protected override void When(DomainEvent @event)
    {
        // Event sourcing support - not implemented in this version
    }
}
