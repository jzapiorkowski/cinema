namespace Cinema.Application.Features.CinemaHalls.Dto;

public class CinemaHallWithDetailsAppResponseDto
{
    public int Id { get; set; }
    public int Number { get; set; }
    public CinemaHallBuildingAppResponseDto CinemaBuilding { get; set; }
    public IEnumerable<CinemaHallScreeningAppResponseDto> Screenings { get; set; }
    public IEnumerable<CinemaHallSeatAppResponseDto> Seats { get; set; }
}