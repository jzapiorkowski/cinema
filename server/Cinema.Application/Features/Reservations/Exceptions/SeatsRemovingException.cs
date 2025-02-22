namespace Cinema.Application.Features.Reservations.Exceptions;

public class SeatsRemovingException : Exception
{
    public SeatsRemovingException(string message) : base(message)
    {
    }
}