using Cinema.Domain.Features.Screenings.Entities;

namespace Cinema.Application.Features.Screenings.Interfaces;

internal interface IScreeningService
{
    public Task<Screening> GetWithDetailsByIdAsync(int id);
    public Task<IEnumerable<Screening>> GetAllWithDetailsAsync(DateTime date);
}