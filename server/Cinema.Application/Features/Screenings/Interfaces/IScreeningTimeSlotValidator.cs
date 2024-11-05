namespace Cinema.Application.Features.Screenings.Interfaces;

internal interface IScreeningTimeSlotValidator
{
    public Task ValidateTimeSlotAsync(int cinemaHallId, DateTime startTime, TimeSpan movieDuration);
}