namespace Cinema.Application.Features.CinemaHalls.Exceptions;

public class InvalidCinemaHallCapacityException : ArgumentException
{
    public InvalidCinemaHallCapacityException(int newCapacity) : base(
        $"Cinema hall has more seats than its desired capacity of {newCapacity}.")
    {
    }
}