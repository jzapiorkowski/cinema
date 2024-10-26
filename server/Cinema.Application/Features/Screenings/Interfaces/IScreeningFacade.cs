using Cinema.Application.Features.Screenings.Dto;

namespace Cinema.Application.Features.Screenings.Interfaces;

public interface IScreeningFacade
{
    public Task<ScreeningWithDetailsAppResponseDto> GetWithDetailsByIdAsync(int id);
    public Task<IEnumerable<ScreeningWithDetailsAppResponseDto>> GetAllWithDetailsAsync(DateTime date);
    public Task<ScreeningAppResponseDto> CreateAsync(CreateScreeningAppDto screening);
}