using Cinema.Domain.Features.Seats.Entities;

namespace Cinema.Application.Features.Seats.Interfaces;

internal interface ISeatService
{
    public Task<Seat> CreateAsync(Seat seat);
}