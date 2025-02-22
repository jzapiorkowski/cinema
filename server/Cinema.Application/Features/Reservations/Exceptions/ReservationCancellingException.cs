namespace Cinema.Application.Features.Reservations.Exceptions;

public class ReservationCancellingException : Exception
{
    public ReservationCancellingException(string message) : base(message)
    {
    }
}