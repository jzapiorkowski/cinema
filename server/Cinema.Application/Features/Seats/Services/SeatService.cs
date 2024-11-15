using Cinema.Application.Features.Seats.Interfaces;
using Cinema.Application.Shared.Exceptions;
using Cinema.Domain.Features.Seats.Entities;
using Cinema.Domain.Features.Seats.Repositories;
using Cinema.Domain.Shared.Interfaces;
using Microsoft.Extensions.Logging;

namespace Cinema.Application.Features.Seats.Services;

internal class SeatService : ISeatService
{
    private readonly ILogger<SeatService> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public SeatService(ILogger<SeatService> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Seat> CreateAsync(Seat seat)
    {
        try
        {
            var createdSeat =
                await _unitOfWork.Repository<Seat, ISeatRepository>().CreateAsync(seat);
            await _unitOfWork.CompleteAsync();

            return createdSeat;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while creating seat");
            throw new AppException($"An error occurred while creating seat", e);
        }
    }
}