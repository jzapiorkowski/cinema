using Cinema.Application.Features.Reservations.Interfaces;
using Cinema.Domain.Features.ReservationsSeats.Entities;

namespace Cinema.Application.Features.Reservations.Builders;

internal class ReservationSeatBuilder : IReservationSeatBuilder
{
    private ReservationSeat _reservationSeat;

    public ReservationSeatBuilder()
    {
        Reset();
    }

    public IReservationSeatBuilder SetReservationId(int reservationId)
    {
        _reservationSeat.ReservationId = reservationId;
        return this;
    }

    public IReservationSeatBuilder SetSeatId(int seatId)
    {
        _reservationSeat.SeatId = seatId;
        return this;
    }

    public ReservationSeat Build()
    {
        var result = _reservationSeat;
        Reset();

        return result;
    }

    private void Reset()
    {
        _reservationSeat = new ReservationSeat();
    }
}