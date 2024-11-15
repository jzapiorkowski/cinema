using Cinema.Application.Features.Screenings.Interfaces;
using Cinema.Domain.Features.Screenings.Entities;

namespace Cinema.Application.Features.Screenings.Builders;

internal class ScreeningBuilder : IScreeningBuilder
{
    private Screening _screening;

    public ScreeningBuilder()
    {
        Reset();
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
        var result = _screening;
        Reset();

        return result;
    }

    private void Reset()
    {
        _screening = new Screening();
    }
}