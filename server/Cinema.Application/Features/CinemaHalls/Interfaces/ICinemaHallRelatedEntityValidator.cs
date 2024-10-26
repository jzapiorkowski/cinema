using Cinema.Domain.Features.CinemaHalls.Entities;

namespace Cinema.Application.Features.CinemaHalls.Interfaces;

internal interface ICinemaHallRelatedEntityValidator
{
    Task<CinemaHall> ValidateEntityAsync(int cinemaHallId);
}