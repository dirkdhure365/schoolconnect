using SchoolConnect.Common.Domain.Primitives;
using SchoolConnect.Identity.Domain.Enums;

namespace SchoolConnect.Identity.Domain.Entities;

public class Permission : Entity
{
    public string Code { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public PermissionCategory Category { get; private set; }

    private Permission() { }

    public Permission(string code, string name, string? description, PermissionCategory category)
    {
        Id = Guid.NewGuid();
        Code = code ?? throw new ArgumentNullException(nameof(code));
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Description = description;
        Category = category;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Update(string name, string? description, PermissionCategory category)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Description = description;
        Category = category;
        UpdatedAt = DateTime.UtcNow;
    }
}
