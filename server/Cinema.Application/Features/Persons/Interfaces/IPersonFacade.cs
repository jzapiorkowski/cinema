using Cinema.Application.Features.Persons.Dto;
using Cinema.Domain.Core.Pagination;

namespace Cinema.Application.Features.Persons.Interfaces;

public interface IPersonFacade
{
    public Task<PaginationResponse<PersonAppResponseDto>> GetAllAsync(PaginationRequest paginationRequest);
    public Task<PersonWithDetailsAppResponseDto> GetByIdAsync(int id);
    public Task DeleteAsync(int id);
    public Task<PersonAppResponseDto> CreateAsync(CreatePersonAppDto person);
    public Task<PersonAppResponseDto> UpdateAsync(int id, UpdatePersonAppDto person);
}