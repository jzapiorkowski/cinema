using Cinema.Domain.Features.Reservations.Entities;
using Cinema.Domain.Features.Reservations.Repositories;
using Cinema.Infrastructure.Core.Data;
using Cinema.Infrastructure.Shared.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Cinema.Infrastructure.Features.Reservations.Repositories;

internal class ReservationRepository : BaseRepository<Reservation>, IReservationRepository
{
    public ReservationRepository(ApplicationDbContext dbContext, ILogger<BaseRepository<Reservation>> logger) : base(
        dbContext, logger)
    {
    }

    protected override IQueryable<Reservation> BuildIncludesQuery(IQueryable<Reservation> query)
    {
        return query
            .Include(r => r.Screening).ThenInclude(s => s.Movie)
            .Include(r => r.Screening).ThenInclude(s => s.CinemaHall)
            .Include(r => r.ReservationSeats).ThenInclude(rs => rs.Seat);
    }
}