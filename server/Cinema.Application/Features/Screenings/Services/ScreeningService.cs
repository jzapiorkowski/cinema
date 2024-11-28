using Cinema.Application.Features.Screenings.Interfaces;
using Cinema.Application.Shared.Exceptions;
using Cinema.Domain.Features.Screenings.Entities;
using Cinema.Domain.Features.Screenings.Repositories;
using Cinema.Domain.Shared.Interfaces;
using Microsoft.Extensions.Logging;

namespace Cinema.Application.Features.Screenings.Services;

internal class ScreeningService : IScreeningService
{
    private readonly ILogger<ScreeningService> _logger;
    private readonly IUnitOfWork _unitOfWork;


    public ScreeningService(ILogger<ScreeningService> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<Screening> GetByIdAsync(int id)
    {
        return await GetByIdAsync(id, true, true);
    }

    public async Task<IEnumerable<Screening>> GetAllWithDetailsAsync(DateTime date)
    {
        try
        {
            var screenings = await _unitOfWork.Repository<Screening, IScreeningRepository>()
                .GetAllWithDetailsAsync(date);

            return screenings;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while retrieving all screenings");
            throw new AppException($"An error occurred while retrieving all screenings", e);
        }
    }

    public async Task<Screening> CreateAsync(Screening screening)
    {
        try
        {
            var createdScreening =
                await _unitOfWork.Repository<Screening, IScreeningRepository>().CreateAsync(screening);
            await _unitOfWork.CompleteAsync();

            return createdScreening;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while creating screening");
            throw new AppException($"An error occurred while creating screening", e);
        }
    }

    public async Task<Screening> UpdateAsync(Screening screening)
    {
        try
        {
            await GetByIdAsync(screening.Id, true, false);

            var updatedScreening =
                _unitOfWork.Repository<Screening, IScreeningRepository>().Update(screening);
            await _unitOfWork.CompleteAsync();

            return updatedScreening;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while updating screening");
            throw new AppException($"An error occurred while updating screening", e);
        }
    }

    public async Task<bool> IsTimeSlotAvailableAsync(int cinemaHallId, DateTime startTime, DateTime endTime)
    {
        try
        {
            if (startTime >= endTime)
                throw new ArgumentException("Start time must be before end time");

            return await _unitOfWork.Repository<Screening, IScreeningRepository>()
                .IsTimeSlotAvailableAsync(cinemaHallId, startTime, endTime);
        }
        catch (ArgumentException)
        {
            throw;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while checking if screening time slot is available");
            throw new AppException($"An error occurred while checking if screening time slot is available", e);
        }
    }

    private async Task<Screening> GetByIdAsync(int id, bool asNoTracking, bool includeAllRelations)
    {
        try
        {
            var screening = await _unitOfWork.Repository<Screening, IScreeningRepository>()
                .GetByIdAsync(id, asNoTracking, includeAllRelations);

            if (screening == null)
            {
                throw new NotFoundException("screening", id);
            }

            return screening;
        }
        catch (NotFoundException)
        {
            throw;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while retrieving screening with id {id}", id);
            throw new AppException($"An error occurred while retrieving screening with id {id}", e);
        }
    }
}