using Cinema.Application.Features.CinemaHalls.Interfaces;
using Cinema.Application.Features.Movies.Interfaces;
using Cinema.Application.Features.Screenings.Interfaces;

namespace Cinema.Application.Features.Screenings.Validators;

internal class ScreeningValidator : IScreeningValidator
{
    private readonly IMovieRelatedEntityValidator _movieRelatedEntityValidator;
    private readonly ICinemaHallRelatedEntityValidator _cinemaHallRelatedEntityValidator;
    private readonly IScreeningTimeSlotValidator _screeningTimeSlotValidator;

    public ScreeningValidator(
        IMovieRelatedEntityValidator movieRelatedEntityValidator,
        ICinemaHallRelatedEntityValidator cinemaHallRelatedEntityValidator,
        IScreeningTimeSlotValidator screeningTimeSlotValidator)
    {
        _movieRelatedEntityValidator = movieRelatedEntityValidator;
        _cinemaHallRelatedEntityValidator = cinemaHallRelatedEntityValidator;
        _screeningTimeSlotValidator = screeningTimeSlotValidator;
    }

    public async Task ValidateAsync(int movieId, int cinemaHallId, DateTime startTime)
    {
        var movie = await _movieRelatedEntityValidator.ValidateEntityAsync(movieId);
        await _cinemaHallRelatedEntityValidator.ValidateEntityAsync(cinemaHallId);
        await _screeningTimeSlotValidator.ValidateTimeSlotAsync(cinemaHallId, startTime, movie.Duration);
    }
}