using SchoolConnect.EducationSystem.Domain.Aggregates;
using SchoolConnect.EducationSystem.Domain.Events;

namespace SchoolConnect.EducationSystem.Domain.Entities;

public class Country : AggregateRoot
{
    public string Name { get; private set; } = string.Empty;
    public string Code { get; private set; } = string.Empty;
    public string Region { get; private set; } = string.Empty;

    private Country() { }

    public static Country Create(string name, string code, string region)
    {
        var country = new Country
        {
            Name = name,
            Code = code,
            Region = region
        };
        
        country.AddDomainEvent(new CountryCreatedEvent
        {
            AggregateId = country.Id,
            EventType = nameof(CountryCreatedEvent),
            Name = name,
            Code = code,
            Region = region
        });

        return country;
    }

    public void Update(string name, string code, string region)
    {
        Name = name;
        Code = code;
        Region = region;
        MarkAsUpdated();

        AddDomainEvent(new CountryUpdatedEvent
        {
            AggregateId = Id,
            EventType = nameof(CountryUpdatedEvent),
            Name = name,
            Code = code,
            Region = region
        });
    }
}

public class CountryCreatedEvent : DomainEvent
{
    public string Name { get; init; } = string.Empty;
    public string Code { get; init; } = string.Empty;
    public string Region { get; init; } = string.Empty;
}

public class CountryUpdatedEvent : DomainEvent
{
    public string Name { get; init; } = string.Empty;
    public string Code { get; init; } = string.Empty;
    public string Region { get; init; } = string.Empty;
}
