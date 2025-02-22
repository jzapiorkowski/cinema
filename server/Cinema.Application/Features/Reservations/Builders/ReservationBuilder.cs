using Cinema.Application.Features.Reservations.Interfaces;
using Cinema.Domain.Features.Reservations.Entities;

namespace Cinema.Application.Features.Reservations.Builders;

internal class ReservationBuilder : IReservationBuilder
{
    private Reservation _reservation;

    public ReservationBuilder()
    {
        Reset();
    }

    public IReservationBuilder SetScreeningId(int screeningId)
    {
        _reservation.ScreeningId = screeningId;
        return this;
    }

    public Reservation Build()
    {
        var result = _reservation;
        Reset();

        return result;
    }

    private void Reset()
    {
        _reservation = new Reservation();
    }
}