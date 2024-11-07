namespace Cinema.Application.Features.CinemaBuildings.Dto;

public class CinemaBuildingWithDetailsAppResponseDto
{
    public int Id { get; set; }
    // TODO to new entity
    public string Address { get; set; }
    public List<CinemaBuildingHallAppResponseDto> CinemaHalls { get; set; }
}