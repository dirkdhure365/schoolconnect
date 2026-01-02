using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.Institution.Domain.Events;
using SchoolConnect.Institution.Domain.Primitives;

namespace SchoolConnect.Institution.Domain.Entities;

public class TeamMember : Entity
{
    public Guid TeamId { get; private set; }
    public Guid StaffMemberId { get; private set; }
    public string? Role { get; private set; }
    public DateTime JoinedAt { get; private set; }
    public DateTime? LeftAt { get; private set; }

    private TeamMember() { }

    public static TeamMember Create(Guid teamId, Guid staffMemberId, string? role = null)
    {
        var member = new TeamMember
        {
            TeamId = teamId,
            StaffMemberId = staffMemberId,
            Role = role,
            JoinedAt = DateTime.UtcNow
        };

        return member;
    }

    public void UpdateRole(string? role)
    {
        Role = role;
        MarkAsUpdated();
    }

    public void Remove()
    {
        LeftAt = DateTime.UtcNow;
        MarkAsUpdated();
    }
}
