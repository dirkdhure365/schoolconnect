namespace SchoolConnect.Communication.Domain.Exceptions;

public class UnauthorizedConversationAccessException : Exception
{
    public UnauthorizedConversationAccessException(Guid userId, Guid conversationId)
        : base($"User '{userId}' is not authorized to access conversation '{conversationId}'.")
    {
    }
}
