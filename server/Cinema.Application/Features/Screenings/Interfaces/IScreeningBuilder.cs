using Cinema.Domain.Features.Screenings.Entities;

namespace Cinema.Application.Features.Screenings.Interfaces;

internal interface IScreeningBuilder
{
    public IScreeningBuilder SetId(int id);
    public IScreeningBuilder SetStartTime(DateTime startTime);
    public IScreeningBuilder SetMovieId(int movieId);
    public IScreeningBuilder SetCinemaHallId(int cinemaHallId);
    public Screening Build();
}