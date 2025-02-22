using Cinema.Domain.Features.Tickets.Entities;
using Cinema.Domain.Features.Tickets.Repositories;
using Cinema.Infrastructure.Core.Data;
using Cinema.Infrastructure.Shared.Repositories;
using Microsoft.Extensions.Logging;

namespace Cinema.Infrastructure.Features.Tickets.Repositories;

internal class TicketRepository : BaseRepository<Ticket>, ITicketRepository
{
    public TicketRepository(ApplicationDbContext dbContext, ILogger<BaseRepository<Ticket>> logger) : base(dbContext,
        logger)
    {
    }

    protected override IQueryable<Ticket> BuildIncludesQuery(IQueryable<Ticket> query)
    {
        return query;
    }
}