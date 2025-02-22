using Cinema.Application.Features.Reservations.Interfaces;
using Cinema.Domain.Features.Tickets.Entities;

namespace Cinema.Application.Features.Reservations.Builders;

internal class TicketBuilder : ITicketBuilder
{
    private Ticket _ticket;

    public TicketBuilder()
    {
        Reset();
    }

    public ITicketBuilder SetReservationSeatId(int reservationSeatId)
    {
        _ticket.ReservationSeatId = reservationSeatId;
        return this;
    }

    public Ticket Build()
    {
        var result = _ticket;
        Reset();

        return result;
    }

    private void Reset()
    {
        _ticket = new Ticket();
    }
}