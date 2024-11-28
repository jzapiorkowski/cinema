using Cinema.Domain.Features.CinemaHalls.Entities;
using Cinema.Domain.Features.CinemaHalls.Repositories;
using Cinema.Infrastructure.Core.Data;
using Cinema.Infrastructure.Shared.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Cinema.Infrastructure.Features.CinemaHalls.Repositories;

internal class CinemaHallRepository : BaseRepository<CinemaHall>, ICinemaHallRepository
{
    public CinemaHallRepository(ApplicationDbContext dbContext, ILogger<CinemaHallRepository> logger
    ) : base(dbContext, logger)
    {
    }

    protected override IQueryable<CinemaHall> BuildIncludesQuery(IQueryable<CinemaHall> query)
    {
        return query
            .Include(ch => ch.Screenings)
            .Include(ch => ch.CinemaBuilding)
            .Include(ch => ch.Seats);
    }
}