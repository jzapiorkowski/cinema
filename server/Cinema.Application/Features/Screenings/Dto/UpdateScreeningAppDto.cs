namespace Cinema.Application.Features.Screenings.Dto;

public class UpdateScreeningAppDto
{
    public DateTime StartTime { get; set; }
    public int MovieId { get; set; }
    public int CinemaHallId { get; set; }
}