namespace Cinema.Application.Features.CinemaHalls.Dto;

public class CreateCinemaHallAppDto
{
    public int CinemaBuildingId { get; set; }
    public int Number { get; set; }
    public int Capacity { get; set; }
    public List<CreateCinemaHallSeatAppDto> Seats { get; set; }
}