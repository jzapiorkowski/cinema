namespace Cinema.Application.Features.Reservations.Exceptions;

public class ReservationConfirmingException : Exception
{
    public ReservationConfirmingException(string message) : base(message)
    {
    }
}