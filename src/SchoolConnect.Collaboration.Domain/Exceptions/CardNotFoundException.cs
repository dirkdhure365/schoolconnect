namespace SchoolConnect.Collaboration.Domain.Exceptions;

public class CardNotFoundException : Exception
{
    public CardNotFoundException(Guid cardId)
        : base($"Card with ID '{cardId}' was not found.")
    {
    }
}
