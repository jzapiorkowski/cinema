using Cinema.Application.Features.Persons.Dto;

namespace Cinema.Application.Features.Persons.Interfaces;

public interface IPersonFacade
{
    public Task<IEnumerable<PersonAppResponseDto>> GetAllAsync();
    public Task<PersonWithDetailsAppResponseDto> GetWithDetailsByIdAsync(int id);
    public Task DeleteAsync(int id);
}