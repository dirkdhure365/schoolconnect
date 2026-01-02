namespace SchoolConnect.Collaboration.Domain.Exceptions;

public class MemberAlreadyExistsException : Exception
{
    public MemberAlreadyExistsException(Guid userId, Guid workspaceId)
        : base($"User '{userId}' is already a member of workspace '{workspaceId}'.")
    {
    }
}
