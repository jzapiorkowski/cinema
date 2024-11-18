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

    public async Task<Seat> UpdateAsync(Seat seat)
    {
        try
        {
            await GetByIdAsync(seat.Id, true, false);

            var createdSeat =
                _unitOfWork.Repository<Seat, ISeatRepository>().Update(seat);
            await _unitOfWork.CompleteAsync();

            return createdSeat;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while updating seat");
            throw new AppException($"An error occurred while updating seat", e);
        }
    }

    public async Task DeleteAsync(int id)
    {
        try
        {
            var seat = await GetByIdAsync(id, false, false);

            _unitOfWork.Repository<Seat, ISeatRepository>().Delete(seat);
            await _unitOfWork.CompleteAsync();
        }
        catch (NotFoundException)
        {
            throw;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while deleting seat with id {id}", id);
            throw new AppException($"An error occurred while deleting seat with id {id}", e);
        }
    }

    public async Task<Seat> GetByIdAsync(int id)
    {
        return await GetByIdAsync(id, true, true);
    }

    private async Task<Seat> GetByIdAsync(int id, bool asNoTracking, bool includeAllRelations)
    {
        try
        {
            var seat = await (includeAllRelations
                ? _unitOfWork.Repository<Seat, ISeatRepository>().GetWithDetailsByIdAsync(id, asNoTracking)
                : _unitOfWork.Repository<Seat, ISeatRepository>().GetByIdAsync(id, asNoTracking));

            if (seat == null)
            {
                throw new NotFoundException("seat", id);
            }

            return seat;
        }
        catch (NotFoundException)
        {
            throw;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while retrieving seat");
            throw new AppException($"An error occurred while retrieving seat", e);
        }
    }
}