using SchoolConnect.EducationSystem.Domain.Aggregates;
using SchoolConnect.EducationSystem.Domain.Events;

namespace SchoolConnect.EducationSystem.Domain.Entities;

public class Subject : AggregateRoot
{
    public string ProgramId { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public string Code { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public bool IsCore { get; private set; }

    private Subject() { }

    public static Subject Create(string programId, string name, string code, string description, bool isCore)
    {
        var subject = new Subject
        {
            ProgramId = programId,
            Name = name,
            Code = code,
            Description = description,
            IsCore = isCore
        };

        subject.AddDomainEvent(new SubjectCreatedEvent
        {
            AggregateId = subject.Id,
            EventType = nameof(SubjectCreatedEvent),
            ProgramId = programId,
            Name = name,
            Code = code,
            Description = description,
            IsCore = isCore
        });

        return subject;
    }

    public void Update(string name, string code, string description, bool isCore)
    {
        Name = name;
        Code = code;
        Description = description;
        IsCore = isCore;
        MarkAsUpdated();

        AddDomainEvent(new SubjectUpdatedEvent
        {
            AggregateId = Id,
            EventType = nameof(SubjectUpdatedEvent),
            Name = name,
            Code = code,
            Description = description,
            IsCore = isCore
        });
    }
}

public class SubjectCreatedEvent : DomainEvent
{
    public string ProgramId { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string Code { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public bool IsCore { get; init; }
}

public class SubjectUpdatedEvent : DomainEvent
{
    public string Name { get; init; } = string.Empty;
    public string Code { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public bool IsCore { get; init; }
}
