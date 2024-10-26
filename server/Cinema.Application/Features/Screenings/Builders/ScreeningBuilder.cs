using Cinema.Application.Features.Screenings.Interfaces;
using Cinema.Domain.Features.Screenings.Entities;

namespace Cinema.Application.Features.Screenings.Builders;

internal class ScreeningBuilder : IScreeningBuilder
{
    private readonly Screening _screening;
    
    public ScreeningBuilder()
    {
        _screening = new Screening();
    }
    
    public IScreeningBuilder SetId(int id)
    {
        _screening.Id = id;
        return this;
    }
    
    public IScreeningBuilder SetStartTime(DateTime startTime)
    {
        _screening.StartTime = startTime;
        return this;
    }
    
    public IScreeningBuilder SetMovieId(int movieId)
    {
        _screening.MovieId = movieId;
        return this;
    }
    
    public IScreeningBuilder SetCinemaHallId(int cinemaHallId)
    {
        _screening.CinemaHallId = cinemaHallId;
        return this;
    }

    public Screening Build()
    {
        return _screening;
    }
}