namespace Cinema.Application.Features.Screenings.Dto;

public class CreateScreeningAppDto
{
    public DateTime StartTime { get; set; }
    public int MovieId { get; set; }
    public int CinemaHallId { get; set; }
}