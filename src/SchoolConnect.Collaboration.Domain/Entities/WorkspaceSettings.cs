namespace SchoolConnect.Collaboration.Domain.Entities;

public class WorkspaceSettings
{
    public bool AllowMemberInvites { get; set; }
    public bool AllowGuestAccess { get; set; }
    public List<string> DefaultBoardLabels { get; set; } = [];
    public string? DefaultBoardBackground { get; set; }
}
