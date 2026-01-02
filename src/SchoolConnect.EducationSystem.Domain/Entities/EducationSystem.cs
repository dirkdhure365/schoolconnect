using SchoolConnect.EducationSystem.Domain.Aggregates;
using SchoolConnect.EducationSystem.Domain.Events;

namespace SchoolConnect.EducationSystem.Domain.Entities;

public class EducationSystem : AggregateRoot
{
    public string CountryId { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;

    private EducationSystem() { }

    public static EducationSystem Create(string countryId, string name, string description)
    {
        var system = new EducationSystem
        {
            CountryId = countryId,
            Name = name,
            Description = description
        };

        system.AddDomainEvent(new EducationSystemCreatedEvent
        {
            AggregateId = system.Id,
            EventType = nameof(EducationSystemCreatedEvent),
            CountryId = countryId,
            Name = name,
            Description = description
        });

        return system;
    }

    public void Update(string name, string description)
    {
        Name = name;
        Description = description;
        MarkAsUpdated();

        AddDomainEvent(new EducationSystemUpdatedEvent
        {
            AggregateId = Id,
            EventType = nameof(EducationSystemUpdatedEvent),
            Name = name,
            Description = description
        });
    }
}

public class EducationSystemCreatedEvent : DomainEvent
{
    public string CountryId { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
}

public class EducationSystemUpdatedEvent : DomainEvent
{
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
}
