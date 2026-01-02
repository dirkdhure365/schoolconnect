namespace SchoolConnect.Collaboration.Domain.Exceptions;

public class InvalidCardMoveException : Exception
{
    public InvalidCardMoveException(string message)
        : base(message)
    {
    }
}
