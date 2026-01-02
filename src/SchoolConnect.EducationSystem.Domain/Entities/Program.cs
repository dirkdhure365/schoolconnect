using SchoolConnect.EducationSystem.Domain.Aggregates;
using SchoolConnect.EducationSystem.Domain.Events;

namespace SchoolConnect.EducationSystem.Domain.Entities;

public class Program : AggregateRoot
{
    public string AssessmentBoardId { get; private set; } = string.Empty;
    public string EducationPhaseId { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public int DurationYears { get; private set; }

    private Program() { }

    public static Program Create(string assessmentBoardId, string educationPhaseId, string name, string description, int durationYears)
    {
        var program = new Program
        {
            AssessmentBoardId = assessmentBoardId,
            EducationPhaseId = educationPhaseId,
            Name = name,
            Description = description,
            DurationYears = durationYears
        };

        program.AddDomainEvent(new ProgramCreatedEvent
        {
            AggregateId = program.Id,
            EventType = nameof(ProgramCreatedEvent),
            AssessmentBoardId = assessmentBoardId,
            EducationPhaseId = educationPhaseId,
            Name = name,
            Description = description,
            DurationYears = durationYears
        });

        return program;
    }

    public void Update(string name, string description, int durationYears)
    {
        Name = name;
        Description = description;
        DurationYears = durationYears;
        MarkAsUpdated();

        AddDomainEvent(new ProgramUpdatedEvent
        {
            AggregateId = Id,
            EventType = nameof(ProgramUpdatedEvent),
            Name = name,
            Description = description,
            DurationYears = durationYears
        });
    }
}

public class ProgramCreatedEvent : DomainEvent
{
    public string AssessmentBoardId { get; init; } = string.Empty;
    public string EducationPhaseId { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public int DurationYears { get; init; }
}

public class ProgramUpdatedEvent : DomainEvent
{
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public int DurationYears { get; init; }
}
