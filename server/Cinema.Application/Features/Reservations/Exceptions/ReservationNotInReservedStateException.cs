using Cinema.Domain.Features.Reservations.Entities;

namespace Cinema.Application.Features.Reservations.Exceptions;

public class ReservationNotInReservedStateException : Exception
{
    public ReservationNotInReservedStateException(int reservationId, ReservationStatus status)
        : base(
            $"Cannot modify reservation {reservationId} because its status is {status}. Only reservations with status RESERVED can be modified.")
    {
    }
}