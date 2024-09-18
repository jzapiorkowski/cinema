using Cinema.Domain.Features.Screenings.Entities;

namespace Cinema.Application.Features.Screenings.Interfaces;

public interface IScreeningService
{
    public Task<Screening> GetWithDetailsByIdAsync(int id);
}