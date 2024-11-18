using Cinema.Domain.Features.Seats.Entities;

namespace Cinema.Application.Features.Seats.Interfaces;

internal interface ISeatService
{
    public Task<Seat> CreateAsync(Seat seat);
    public Task<Seat> UpdateAsync(Seat seat);
    public Task DeleteAsync(int seatId);
    public Task<Seat> GetByIdAsync(int seatId);
}