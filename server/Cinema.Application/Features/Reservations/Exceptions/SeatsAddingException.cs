namespace Cinema.Application.Features.Reservations.Exceptions;

public class SeatsAddingException : Exception
{
    public SeatsAddingException(string message) : base(message)
    {
    }
}