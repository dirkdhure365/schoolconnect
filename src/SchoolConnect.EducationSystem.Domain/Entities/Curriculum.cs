using SchoolConnect.EducationSystem.Domain.Aggregates;
using SchoolConnect.EducationSystem.Domain.Events;

namespace SchoolConnect.EducationSystem.Domain.Entities;

public class Curriculum : AggregateRoot
{
    public string SubjectId { get; private set; } = string.Empty;
    public string Title { get; private set; } = string.Empty;
    public string Content { get; private set; } = string.Empty;
    public string LearningObjectives { get; private set; } = string.Empty;
    public string Assessment { get; private set; } = string.Empty;
    public int Year { get; private set; }

    private Curriculum() { }

    public static Curriculum Create(string subjectId, string title, string content, string learningObjectives, string assessment, int year)
    {
        var curriculum = new Curriculum
        {
            SubjectId = subjectId,
            Title = title,
            Content = content,
            LearningObjectives = learningObjectives,
            Assessment = assessment,
            Year = year
        };

        curriculum.AddDomainEvent(new CurriculumCreatedEvent
        {
            AggregateId = curriculum.Id,
            EventType = nameof(CurriculumCreatedEvent),
            SubjectId = subjectId,
            Title = title,
            Content = content,
            LearningObjectives = learningObjectives,
            Assessment = assessment,
            Year = year
        });

        return curriculum;
    }

    public void Update(string title, string content, string learningObjectives, string assessment, int year)
    {
        Title = title;
        Content = content;
        LearningObjectives = learningObjectives;
        Assessment = assessment;
        Year = year;
        MarkAsUpdated();

        AddDomainEvent(new CurriculumUpdatedEvent
        {
            AggregateId = Id,
            EventType = nameof(CurriculumUpdatedEvent),
            Title = title,
            Content = content,
            LearningObjectives = learningObjectives,
            Assessment = assessment,
            Year = year
        });
    }
}

public class CurriculumCreatedEvent : DomainEvent
{
    public string SubjectId { get; init; } = string.Empty;
    public string Title { get; init; } = string.Empty;
    public string Content { get; init; } = string.Empty;
    public string LearningObjectives { get; init; } = string.Empty;
    public string Assessment { get; init; } = string.Empty;
    public int Year { get; init; }
}

public class CurriculumUpdatedEvent : DomainEvent
{
    public string Title { get; init; } = string.Empty;
    public string Content { get; init; } = string.Empty;
    public string LearningObjectives { get; init; } = string.Empty;
    public string Assessment { get; init; } = string.Empty;
    public int Year { get; init; }
}
