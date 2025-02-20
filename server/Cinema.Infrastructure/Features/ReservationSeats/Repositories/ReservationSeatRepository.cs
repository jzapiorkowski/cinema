using System.Linq.Expressions;
using Cinema.Domain.Features.ReservationsSeats.Entities;
using Cinema.Domain.Features.ReservationsSeats.Repositories;
using Cinema.Infrastructure.Core.Data;
using Cinema.Infrastructure.Shared.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Cinema.Infrastructure.Features.ReservationSeats.Repositories;

internal class ReservationSeatRepository : BaseRepository<ReservationSeat>, IReservationSeatRepository
{
    private readonly ApplicationDbContext _dbContext;


    public ReservationSeatRepository(ApplicationDbContext dbContext, ILogger<BaseRepository<ReservationSeat>> logger) :
        base(dbContext, logger)
    {
        _dbContext = dbContext;
    }

    protected override IQueryable<ReservationSeat> BuildIncludesQuery(IQueryable<ReservationSeat> query)
    {
        return query
            .Include(rs => rs.Reservation).ThenInclude(r => r.Screening)
            .Include(rs => rs.Seat)
            .Include(rs => rs.Ticket);
    }

    public async Task<bool> ExistsAsync(Expression<Func<ReservationSeat, bool>> predicate)
    {
        return await ExecuteDbOperation(async () => await _dbContext.Set<ReservationSeat>().AnyAsync(predicate),
            "checking if reservation seat exists");
    }

    public async Task<ReservationSeat?> GetByReservationIdAndSeatIdAsync(int reservationId, int seatId)
    {
        return await ExecuteDbOperation(async () =>
        {
            IQueryable<ReservationSeat> query = _dbContext.Set<ReservationSeat>();
            query = BuildIncludesQuery(query);

            return await query.FirstOrDefaultAsync(rs => rs.ReservationId == reservationId && rs.SeatId == seatId);
        }, "getting reservation seat by reservation id and seat id");
    }
    
    public async Task<List<int>> GetReservedSeatIdsForScreeningAsync(int screeningId)
    {
        return await ExecuteDbOperation(async () =>
        {
            return await _dbContext.Set<ReservationSeat>()
                .Where(rs => rs.Reservation.ScreeningId == screeningId)
                .Select(rs => rs.SeatId)
                .ToListAsync();
        }, "getting reserved seat IDs for screening");
    }
}