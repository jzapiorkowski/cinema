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

    public async Task<Screening> GetWithDetailsByIdAsync(int id)
    {
        try
        {
            var screening = await _unitOfWork.Repository<Screening, IScreeningRepository>().GetWithDetailsByIdAsync(id);

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