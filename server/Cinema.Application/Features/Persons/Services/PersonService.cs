using Cinema.Application.Features.Persons.Interfaces;
using Cinema.Application.Shared.Exceptions;
using Cinema.Domain.Features.Persons.Entities;
using Cinema.Domain.Features.Persons.Repositories;
using Cinema.Domain.Shared.Interfaces;
using Microsoft.Extensions.Logging;

namespace Cinema.Application.Features.Persons.Services;

internal class PersonService : IPersonService
{
    private readonly ILogger<PersonService> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public PersonService(ILogger<PersonService> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<Person>> GetByIdsAsync(IEnumerable<int> ids)
    {
        try
        {
            return await _unitOfWork.Repository<Person, IPersonRepository>().GetByIdsAsync(ids);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while retrieving people with ids {ids}.", ids);
            throw new AppException($"An error occurred while retrieving people with ids {ids}.", e);
        }
    }
}