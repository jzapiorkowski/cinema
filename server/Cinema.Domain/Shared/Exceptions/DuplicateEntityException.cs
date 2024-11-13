namespace Cinema.Domain.Shared.Exceptions;

public class DuplicateEntityException : Exception
{
    public DuplicateEntityException(string message) : base(message)
    {
    }

    public DuplicateEntityException(string message, Exception inner) : base(message, inner)
    {
    }
}