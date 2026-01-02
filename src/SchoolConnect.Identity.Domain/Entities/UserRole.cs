using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Identity.Domain.Entities;

public class UserRole : Entity
{
    public Guid UserId { get; private set; }
    public Guid RoleId { get; private set; }
    public Guid? InstituteId { get; private set; }
    public Guid? CentreId { get; private set; }
    public DateTime GrantedAt { get; private set; }
    public Guid GrantedBy { get; private set; }

    private UserRole() { }

    public UserRole(Guid userId, Guid roleId, Guid grantedBy, Guid? instituteId = null, Guid? centreId = null)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        RoleId = roleId;
        GrantedBy = grantedBy;
        InstituteId = instituteId;
        CentreId = centreId;
        GrantedAt = DateTime.UtcNow;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}
