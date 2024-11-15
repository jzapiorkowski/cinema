namespace Cinema.Application.Features.CinemaHalls.Exceptions;

public class CinemaHallCapacityExceededException : ArgumentException
{
    public CinemaHallCapacityExceededException(int capacity, int requestedSeats) : base(
        $"Cannot add {requestedSeats} seats to the cinema hall. The capacity is {capacity}.")
    {
    }
}