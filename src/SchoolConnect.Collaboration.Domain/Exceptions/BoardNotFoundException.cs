namespace SchoolConnect.Collaboration.Domain.Exceptions;

public class BoardNotFoundException : Exception
{
    public BoardNotFoundException(Guid boardId)
        : base($"Board with ID '{boardId}' was not found.")
    {
    }
}
