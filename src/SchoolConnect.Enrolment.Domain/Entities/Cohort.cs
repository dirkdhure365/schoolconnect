using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.Enrolment.Domain.Enums;
using SchoolConnect.Enrolment.Domain.Events;

namespace SchoolConnect.Enrolment.Domain.Entities;

public class Cohort : AggregateRoot
{
    public Guid StreamId { get; private set; }
    public Guid CentreId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public int Capacity { get; private set; }
    public int CurrentCount { get; private set; }
    public Guid? CohortAdvisorId { get; private set; }
    public CohortStatus Status { get; private set; }

    private Cohort() { }

    public static Cohort Create(
        Guid streamId,
        Guid centreId,
        string name,
        int capacity,
        string? description = null,
        Guid? cohortAdvisorId = null)
    {
        var cohort = new Cohort
        {
            StreamId = streamId,
            CentreId = centreId,
            Name = name,
            Description = description,
            Capacity = capacity,
            CurrentCount = 0,
            CohortAdvisorId = cohortAdvisorId,
            Status = CohortStatus.Active
        };

        cohort.AddDomainEvent(new CohortCreatedEvent
        {
            AggregateId = cohort.Id,
            AggregateType = nameof(Cohort),
            StreamId = streamId,
            CentreId = centreId,
            Name = name,
            Capacity = capacity
        });

        return cohort;
    }

    public void Update(
        string name,
        int capacity,
        string? description = null,
        Guid? cohortAdvisorId = null)
    {
        Name = name;
        Description = description;
        Capacity = capacity;
        CohortAdvisorId = cohortAdvisorId;
        UpdatedAt = DateTime.UtcNow;
    }

    public void IncrementCount()
    {
        CurrentCount++;
        UpdatedAt = DateTime.UtcNow;
    }

    public void DecrementCount()
    {
        if (CurrentCount > 0)
        {
            CurrentCount--;
            UpdatedAt = DateTime.UtcNow;
        }
    }

    public void Graduate()
    {
        Status = CohortStatus.Graduated;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        Status = CohortStatus.Inactive;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Archive()
    {
        Status = CohortStatus.Archived;
        UpdatedAt = DateTime.UtcNow;
    }

    protected override void When(DomainEvent @event)
    {
        // Event sourcing support - not implemented in this version
    }
}
