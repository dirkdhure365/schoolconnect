namespace SchoolConnect.Collaboration.Domain.Exceptions;

public class UnauthorizedWorkspaceAccessException : Exception
{
    public UnauthorizedWorkspaceAccessException(Guid workspaceId, Guid userId)
        : base($"User '{userId}' is not authorized to access workspace '{workspaceId}'.")
    {
    }
}
