using Cinema.Application.Features.CinemaBuildings.Interfaces;
using Cinema.Application.Shared.Exceptions;
using Cinema.Domain.Features.CinemaBuildings.Entities;
using Cinema.Domain.Features.CinemaBuildings.Repositories;
using Cinema.Domain.Shared.Interfaces;
using Microsoft.Extensions.Logging;

namespace Cinema.Application.Features.CinemaBuildings.Services;

internal class CinemaBuildingService : ICinemaBuildingService
{
    private readonly ILogger<CinemaBuildingService> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public CinemaBuildingService(ILogger<CinemaBuildingService> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<CinemaBuilding> CreateAsync(CinemaBuilding cinemaBuilding)
    {
        try
        {
            var createdCinemaBuilding =
                await _unitOfWork.Repository<CinemaBuilding, ICinemaBuildingRepository>().CreateAsync(cinemaBuilding);
            await _unitOfWork.CompleteAsync();
            return createdCinemaBuilding;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while creating the cinema building.");
            throw new AppException("An error occurred while creating the cinema building.", e);
        }
    }

    public async Task<IEnumerable<CinemaBuilding>> GetAllAsync()
    {
        try
        {
            return await _unitOfWork.Repository<CinemaBuilding, ICinemaBuildingRepository>().GetAllAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while retrieving the cinema buildings");
            throw new AppException("An error occurred while retrieving the cinema buildings", e);
        }
    }

    public async Task DeleteAsync(int id)
    {
        try
        {
            var cinemaBuilding = await GetByIdAsync(id);

            _unitOfWork.Repository<CinemaBuilding, ICinemaBuildingRepository>().Delete(cinemaBuilding);
            await _unitOfWork.CompleteAsync();
        }
        catch (NotFoundException)
        {
            throw;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while deleting the cinema building with id {id}.", id);
            throw new AppException($"An error occurred while deleting the cinema building with id {id}.", e);
        }
    }
    
    public async Task<CinemaBuilding?> GetByIdAsync(int id)
    {
        return await GetByIdAsync(id, true, true);
    }

    private async Task<CinemaBuilding?> GetByIdAsync(int id, bool asNoTracking, bool includeAllRelations)
    {
        try
        {
            var cinemaBuilding =
                await (includeAllRelations
                    ? _unitOfWork.Repository<CinemaBuilding, ICinemaBuildingRepository>()
                        .GetWithDetailsByIdAsync(id, asNoTracking)
                    : _unitOfWork.Repository<CinemaBuilding, ICinemaBuildingRepository>()
                        .GetByIdAsync(id, asNoTracking));

            if (cinemaBuilding == null)
            {
                throw new NotFoundException("cinema building", id);
            }

            return cinemaBuilding;
        }
        catch (NotFoundException)
        {
            throw;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while retrieving the cinema building with id {id}.", id);
            throw new AppException($"An error occurred while retrieving the cinema building with id {id}.", e);
        }
    }
}