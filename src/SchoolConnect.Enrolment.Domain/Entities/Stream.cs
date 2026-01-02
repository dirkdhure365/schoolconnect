using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.Enrolment.Domain.Enums;
using SchoolConnect.Enrolment.Domain.Events;

namespace SchoolConnect.Enrolment.Domain.Entities;

public class Stream : AggregateRoot
{
    public Guid InstituteId { get; private set; }
    public Guid ProgramOfferingId { get; private set; }
    public string ProgramOfferingName { get; private set; } = string.Empty;
    public int Year { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public StreamStatus Status { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime ExpectedEndDate { get; private set; }

    private Stream() { }

    public static Stream Create(
        Guid instituteId,
        Guid programOfferingId,
        string programOfferingName,
        int year,
        string name,
        DateTime startDate,
        DateTime expectedEndDate,
        string? description = null)
    {
        var stream = new Stream
        {
            InstituteId = instituteId,
            ProgramOfferingId = programOfferingId,
            ProgramOfferingName = programOfferingName,
            Year = year,
            Name = name,
            Description = description,
            Status = StreamStatus.Planned,
            StartDate = startDate,
            ExpectedEndDate = expectedEndDate
        };

        stream.AddDomainEvent(new StreamCreatedEvent
        {
            AggregateId = stream.Id,
            AggregateType = nameof(Stream),
            InstituteId = instituteId,
            ProgramOfferingId = programOfferingId,
            Name = name,
            Year = year
        });

        return stream;
    }

    public void Update(
        string name,
        DateTime startDate,
        DateTime expectedEndDate,
        string? description = null)
    {
        Name = name;
        Description = description;
        StartDate = startDate;
        ExpectedEndDate = expectedEndDate;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Activate()
    {
        Status = StreamStatus.Active;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Complete()
    {
        Status = StreamStatus.Completed;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Archive()
    {
        Status = StreamStatus.Archived;
        UpdatedAt = DateTime.UtcNow;
    }

    protected override void When(DomainEvent @event)
    {
        // Event sourcing support - not implemented in this version
    }
}
