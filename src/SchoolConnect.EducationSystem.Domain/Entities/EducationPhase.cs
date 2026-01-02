using SchoolConnect.EducationSystem.Domain.Aggregates;
using SchoolConnect.EducationSystem.Domain.Events;

namespace SchoolConnect.EducationSystem.Domain.Entities;

public class EducationPhase : AggregateRoot
{
    public string EducationSystemId { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public int StartAge { get; private set; }
    public int EndAge { get; private set; }

    private EducationPhase() { }

    public static EducationPhase Create(string educationSystemId, string name, string description, int startAge, int endAge)
    {
        var phase = new EducationPhase
        {
            EducationSystemId = educationSystemId,
            Name = name,
            Description = description,
            StartAge = startAge,
            EndAge = endAge
        };

        phase.AddDomainEvent(new EducationPhaseCreatedEvent
        {
            AggregateId = phase.Id,
            EventType = nameof(EducationPhaseCreatedEvent),
            EducationSystemId = educationSystemId,
            Name = name,
            Description = description,
            StartAge = startAge,
            EndAge = endAge
        });

        return phase;
    }

    public void Update(string name, string description, int startAge, int endAge)
    {
        Name = name;
        Description = description;
        StartAge = startAge;
        EndAge = endAge;
        MarkAsUpdated();

        AddDomainEvent(new EducationPhaseUpdatedEvent
        {
            AggregateId = Id,
            EventType = nameof(EducationPhaseUpdatedEvent),
            Name = name,
            Description = description,
            StartAge = startAge,
            EndAge = endAge
        });
    }
}

public class EducationPhaseCreatedEvent : DomainEvent
{
    public string EducationSystemId { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public int StartAge { get; init; }
    public int EndAge { get; init; }
}

public class EducationPhaseUpdatedEvent : DomainEvent
{
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public int StartAge { get; init; }
    public int EndAge { get; init; }
}
