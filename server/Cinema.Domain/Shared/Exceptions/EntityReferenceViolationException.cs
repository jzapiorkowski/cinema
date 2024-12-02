namespace Cinema.Domain.Shared.Exceptions;

public class EntityReferenceViolationException : Exception
{
    public EntityReferenceViolationException(string message) : base(message)
    {
    }

    public EntityReferenceViolationException(string message, Exception inner) : base(message, inner)
    {
    }
}