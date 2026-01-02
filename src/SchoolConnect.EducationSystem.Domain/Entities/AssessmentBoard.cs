using SchoolConnect.EducationSystem.Domain.Aggregates;
using SchoolConnect.EducationSystem.Domain.Events;

namespace SchoolConnect.EducationSystem.Domain.Entities;

public class AssessmentBoard : AggregateRoot
{
    public string EducationSystemId { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public string Abbreviation { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;

    private AssessmentBoard() { }

    public static AssessmentBoard Create(string educationSystemId, string name, string abbreviation, string description)
    {
        var board = new AssessmentBoard
        {
            EducationSystemId = educationSystemId,
            Name = name,
            Abbreviation = abbreviation,
            Description = description
        };

        board.AddDomainEvent(new AssessmentBoardCreatedEvent
        {
            AggregateId = board.Id,
            EventType = nameof(AssessmentBoardCreatedEvent),
            EducationSystemId = educationSystemId,
            Name = name,
            Abbreviation = abbreviation,
            Description = description
        });

        return board;
    }

    public void Update(string name, string abbreviation, string description)
    {
        Name = name;
        Abbreviation = abbreviation;
        Description = description;
        MarkAsUpdated();

        AddDomainEvent(new AssessmentBoardUpdatedEvent
        {
            AggregateId = Id,
            EventType = nameof(AssessmentBoardUpdatedEvent),
            Name = name,
            Abbreviation = abbreviation,
            Description = description
        });
    }
}

public class AssessmentBoardCreatedEvent : DomainEvent
{
    public string EducationSystemId { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string Abbreviation { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
}

public class AssessmentBoardUpdatedEvent : DomainEvent
{
    public string Name { get; init; } = string.Empty;
    public string Abbreviation { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
}
