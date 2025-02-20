using Cinema.Domain.Features.Reservations.Entities;

namespace Cinema.Application.Features.Reservations.Interfaces;

internal interface IReservationBuilder
{
    public IReservationBuilder SetScreeningId(int screeningId);
    public Reservation Build();
}