using Cinema.Domain.Features.Tickets.Entities;
using Cinema.Domain.Shared.Interfaces;

namespace Cinema.Domain.Features.Tickets.Repositories;

public interface ITicketRepository : IBaseRepository<Ticket>
{
}