namespace SchoolConnect.Collaboration.Domain.Exceptions;

public class WorkspaceNotFoundException : Exception
{
    public WorkspaceNotFoundException(Guid workspaceId)
        : base($"Workspace with ID '{workspaceId}' was not found.")
    {
    }
}
