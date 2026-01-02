namespace SchoolConnect.Communication.Domain.Exceptions;

public class ConversationNotFoundException : Exception
{
    public ConversationNotFoundException(Guid conversationId)
        : base($"Conversation with ID '{conversationId}' was not found.")
    {
    }
}
