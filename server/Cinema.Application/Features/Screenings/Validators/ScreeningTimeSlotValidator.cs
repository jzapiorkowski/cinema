using System.ComponentModel.DataAnnotations;
using Cinema.Application.Features.Screenings.Interfaces;

namespace Cinema.Application.Features.Screenings.Validators;

internal class ScreeningTimeSlotValidator : IScreeningTimeSlotValidator
{
    private readonly IScreeningService _screeningService;

    public ScreeningTimeSlotValidator(IScreeningService screeningService)
    {
        _screeningService = screeningService;
    }

    public async Task ValidateTimeSlotAsync(int cinemaHallId, DateTime startTime, TimeSpan movieDuration)
    {
        var endTime = startTime.Add(movieDuration);
        var isAvailable = await _screeningService.IsTimeSlotAvailableAsync(cinemaHallId, startTime, endTime);

        if (!isAvailable)
        {
            throw new ValidationException("Selected time slot is not available for this cinema hall.");
        }
    }
}