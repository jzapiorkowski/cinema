using Cinema.Application.Features.Persons.Interfaces;
using Cinema.Application.Shared.Exceptions;
using Cinema.Domain.Core.Pagination;
using Cinema.Domain.Features.Persons.Entities;
using Cinema.Domain.Features.Persons.Repositories;
using Cinema.Domain.Shared.Exceptions;
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

    public async Task<PaginationResponse<Person>> GetAllAsync(PaginationRequest paginationRequest)
    {
        try
        {
            return await _unitOfWork.Repository<Person, IPersonRepository>().GetAllAsync(paginationRequest);
        }
        catch (InvalidSortByException)
        {
            throw;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while retrieving people");
            throw new AppException($"An error occurred while retrieving people", e);
        }
    }

    public async Task DeleteAsync(int id)
    {
        try
        {
            var person = await GetByIdAsync(id);

            await _unitOfWork.Repository<Person, IPersonRepository>().DeleteAsync(person);
        }
        catch (NotFoundException)
        {
            throw;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while deleting person with id {id}", id);
            throw new AppException($"An error occurred while deleting person with id {id}", e);
        }
    }

    public async Task<Person> CreateAsync(Person person)
    {
        try
        {
            var createdPerson = await _unitOfWork.Repository<Person, IPersonRepository>().CreateAsync(person);

            return createdPerson;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while creating person");
            throw new AppException($"An error occurred while creating person", e);
        }
    }

    public async Task<Person> UpdateAsync(Person person)
    {
        try
        {
            await GetByIdAsync(person.Id, true, false);

            var updatedPerson = await _unitOfWork.Repository<Person, IPersonRepository>().UpdateAsync(person);

            return updatedPerson;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while updating person with id {id}", person.Id);
            throw new AppException($"An error occurred while updating person with id {person.Id}", e);
        }
    }

    public async Task<Person> GetByIdAsync(int id)
    {
        return await GetByIdAsync(id, true, true);
    }


    private async Task<Person> GetByIdAsync(int id, bool asNoTracking, bool includeAllRelations)
    {
        try
        {
            var person = await _unitOfWork.Repository<Person, IPersonRepository>()
                .GetByIdAsync(id, asNoTracking, includeAllRelations);

            if (person == null)
            {
                throw new NotFoundException("person", id);
            }

            return person;
        }
        catch (NotFoundException)
        {
            throw;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while retrieving person with id {id}", id);
            throw new AppException($"An error occurred while retrieving person with id {id}", e);
        }
    }
}