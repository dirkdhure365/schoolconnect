using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.Institution.Domain.Enums;
using SchoolConnect.Institution.Domain.Events;

namespace SchoolConnect.Institution.Domain.Entities;

public class Team : AggregateRoot
{
    public Guid InstituteId { get; private set; }
    public Guid? CentreId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public TeamType Type { get; private set; }
    public Guid? LeaderId { get; private set; }

    private Team() { }

    public static Team Create(
        Guid instituteId,
        string name,
        TeamType type,
        Guid? centreId = null,
        string? description = null,
        Guid? leaderId = null)
    {
        var team = new Team
        {
            InstituteId = instituteId,
            CentreId = centreId,
            Name = name,
            Description = description,
            Type = type,
            LeaderId = leaderId
        };

        team.AddDomainEvent(new TeamCreatedEvent
        {
            AggregateId = team.Id,
            AggregateType = nameof(Team),
            InstituteId = instituteId,
            Name = name,
            Type = type
        });

        return team;
    }

    public void Update(
        string name,
        string? description = null,
        Guid? leaderId = null)
    {
        Name = name;
        Description = description;
        LeaderId = leaderId;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new TeamUpdatedEvent
        {
            AggregateId = Id,
            AggregateType = nameof(Team),
            Name = name
        });
    }

    public void Delete()
    {
        AddDomainEvent(new TeamDeletedEvent
        {
            AggregateId = Id,
            AggregateType = nameof(Team)
        });
    }

    protected override void When(DomainEvent @event)
    {
        // Event sourcing support - not implemented in this version
    }
}
