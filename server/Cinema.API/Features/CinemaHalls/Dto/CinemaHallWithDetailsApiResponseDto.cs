namespace Cinema.API.Features.CinemaHalls.Dto;

public class CinemaHallWithDetailsApiResponseDto
{
    public int Id { get; set; }
    public int Number { get; set; }
    public IEnumerable<CinemaHallScreeningApiResponseDto> Screenings { get; set; }
    public CinemaHallBuildingApiResponseDto CinemaBuilding { get; set; }
    public IEnumerable<CinemaHallSeatApiResponseDto> Seats { get; set; }
}