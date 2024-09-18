using Cinema.Application.Features.Screenings.Dto;

namespace Cinema.Application.Features.Screenings.Interfaces;

public interface IScreeningFacade
{
    public Task<ScreeningWithDetailsAppResponseDto> GetWithDetailsByIdAsync(int id);
}