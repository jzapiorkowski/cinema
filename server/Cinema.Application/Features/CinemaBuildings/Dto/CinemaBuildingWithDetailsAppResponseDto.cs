namespace Cinema.Application.Features.CinemaBuildings.Dto;

public class CinemaBuildingWithDetailsAppResponseDto
{
    public int Id { get; set; }
    // TODO to new entity
    public string Address { get; set; }
    public IEnumerable<CinemaBuildingHallAppResponseDto> CinemaHalls { get; set; }
}