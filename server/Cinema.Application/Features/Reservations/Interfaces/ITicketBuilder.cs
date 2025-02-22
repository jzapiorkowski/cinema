using Cinema.Domain.Features.Tickets.Entities;

namespace Cinema.Application.Features.Reservations.Interfaces;

internal interface ITicketBuilder
{
    public ITicketBuilder SetReservationSeatId(int reservationSeatId);
    public Ticket Build();
}