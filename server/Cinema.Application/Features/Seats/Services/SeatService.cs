using Cinema.Application.Features.Seats.Interfaces;
using Cinema.Application.Shared.Exceptions;
using Cinema.Domain.Features.Seats.Entities;
using Cinema.Domain.Features.Seats.Repositories;
using Cinema.Domain.Shared.Exceptions;
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

            return createdSeat;
        }
        catch (DuplicateEntityException)
        {
            throw;
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
                await _unitOfWork.Repository<Seat, ISeatRepository>().UpdateAsync(seat);

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

            await _unitOfWork.Repository<Seat, ISeatRepository>().DeleteAsync(seat);
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
            var seat = await _unitOfWork.Repository<Seat, ISeatRepository>()
                .GetByIdAsync(id, asNoTracking, includeAllRelations);

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