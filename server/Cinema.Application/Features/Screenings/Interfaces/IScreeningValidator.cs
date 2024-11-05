namespace Cinema.Application.Features.Screenings.Interfaces;

internal interface IScreeningValidator
{
    public Task ValidateAsync(int movieId, int cinemaHallId, DateTime startTime);
}