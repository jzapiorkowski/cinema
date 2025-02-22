namespace Cinema.Application.Features.Reservations.Exceptions;

public class SeatAlreadyOccupiedException : Exception
{
    public SeatAlreadyOccupiedException(int seatId, int screeningId)
        : base($"Seat {seatId} is already occupied for screening {screeningId}.")
    {
    }
}