using SchoolConnect.Common.Domain.Primitives;

namespace SchoolConnect.Collaboration.Domain.ValueObjects;

public class MemberPermissions : ValueObject
{
    public bool CanCreateBoards { get; private set; }
    public bool CanDeleteBoards { get; private set; }
    public bool CanInviteMembers { get; private set; }
    public bool CanRemoveMembers { get; private set; }
    public bool CanEditSettings { get; private set; }

    private MemberPermissions() { }

    public static MemberPermissions Create(
        bool canCreateBoards,
        bool canDeleteBoards,
        bool canInviteMembers,
        bool canRemoveMembers,
        bool canEditSettings)
    {
        return new MemberPermissions
        {
            CanCreateBoards = canCreateBoards,
            CanDeleteBoards = canDeleteBoards,
            CanInviteMembers = canInviteMembers,
            CanRemoveMembers = canRemoveMembers,
            CanEditSettings = canEditSettings
        };
    }

    public static MemberPermissions OwnerPermissions() => Create(true, true, true, true, true);
    public static MemberPermissions AdminPermissions() => Create(true, true, true, true, true);
    public static MemberPermissions DefaultMemberPermissions() => Create(true, false, false, false, false);
    public static MemberPermissions GuestPermissions() => Create(false, false, false, false, false);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return CanCreateBoards;
        yield return CanDeleteBoards;
        yield return CanInviteMembers;
        yield return CanRemoveMembers;
        yield return CanEditSettings;
    }
}
