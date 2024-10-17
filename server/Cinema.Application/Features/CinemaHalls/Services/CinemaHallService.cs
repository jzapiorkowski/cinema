using Cinema.Application.Features.CinemaHalls.Interfaces;
using Cinema.Application.Shared.Exceptions;
using Cinema.Domain.Features.CinemaHalls.Entities;
using Cinema.Domain.Features.CinemaHalls.Repositories;
using Cinema.Domain.Shared.Interfaces;
using Microsoft.Extensions.Logging;

namespace Cinema.Application.Features.CinemaHalls.Services;

internal class CinemaHallService : ICinemaHallService
{
    private readonly ILogger<CinemaHallService> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public CinemaHallService(ILogger<CinemaHallService> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<CinemaHall> CreateAsync(CinemaHall cinemaHall)
    {
        try
        {
            var createdCinemaHall =
                await _unitOfWork.Repository<CinemaHall, ICinemaHallRepository>().CreateAsync(cinemaHall);
            await _unitOfWork.CompleteAsync();
            return createdCinemaHall;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while creating the cinema hall.");
            throw new AppException("An error occurred while creating the cinema hall.", e);
        }
    }

    public async Task<IEnumerable<CinemaHall>> GetAllAsync()
    {
        try
        {
            return await _unitOfWork.Repository<CinemaHall, ICinemaHallRepository>().GetAllAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while retrieving the cinema halls");
            throw new AppException("An error occurred while retrieving the cinema halls", e);
        }
    }

    public async Task DeleteAsync(int id)
    {
        try
        {
            var cinemaHall = await GetByIdAsync(id);

            _unitOfWork.Repository<CinemaHall, ICinemaHallRepository>().Delete(cinemaHall);
            await _unitOfWork.CompleteAsync();
        }
        catch (NotFoundException)
        {
            throw;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while deleting the cinema hall with id {id}.", id);
            throw new AppException($"An error occurred while deleting the cinema hall with id {id}.", e);
        }
    }

    public async Task<CinemaHall> UpdateAsync(CinemaHall cinemaHall)
    {
        try
        {
            var updatedCinemaHall = _unitOfWork.Repository<CinemaHall, ICinemaHallRepository>().Update(cinemaHall);
            await _unitOfWork.CompleteAsync();

            return updatedCinemaHall;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while updating the cinema hall with id {id}.", cinemaHall.Id);
            throw new AppException($"An error occurred while updating the cinema hall with id {cinemaHall.Id}.", e);
        }
    }

    public async Task<CinemaHall> GetByIdAsync(int id)
    {
        try
        {
            var cinemaHall = await _unitOfWork.Repository<CinemaHall, ICinemaHallRepository>().GetByIdAsync(id);

            if (cinemaHall == null)
            {
                throw new NotFoundException("cinema hall", id);
            }

            return cinemaHall;
        }
        catch (NotFoundException)
        {
            throw;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while retrieving the cinema hall with id {id}.", id);
            throw new AppException($"An error occurred while retrieving the cinema hall with id {id}.", e);
        }
    }
}