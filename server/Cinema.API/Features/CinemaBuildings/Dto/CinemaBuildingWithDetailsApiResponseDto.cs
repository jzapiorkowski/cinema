namespace Cinema.API.Features.CinemaBuildings.Dto;

public class CinemaBuildingWithDetailsApiResponseDto
{
    public int Id { get; set; }
    // TODO to new entity
    public string Address { get; set; }
    public IEnumerable<CinemaBuildingHallApiResponseDto> CinemaHalls { get; set; }
}