using Cinema.Application.Features.CinemaHalls.Interfaces;
using Cinema.Application.Shared.Validators;
using Cinema.Domain.Features.CinemaHalls.Entities;

namespace Cinema.Application.Features.CinemaHalls.Validators;

internal class CinemaHallRelatedEntityValidator : RelatedEntityExistenceValidator<CinemaHall, ICinemaHallService>,
    ICinemaHallRelatedEntityValidator
{
    public CinemaHallRelatedEntityValidator(ICinemaHallService cinemaHallService) : base(cinemaHallService)
    {
    }

    public async Task<CinemaHall> ValidateEntityAsync(int cinemaHallId)
    {
        return await base.ValidateEntityAsync(cinemaHallId, "cinema hall");
    }

    protected override async Task<CinemaHall> GetEntityByIdAsync(int id)
    {
        return await _service.GetByIdAsync(id);
    }

    protected override int GetEntityId(CinemaHall entity)
    {
        return entity.Id;
    }
}