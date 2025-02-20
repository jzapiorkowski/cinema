namespace Cinema.API.Features.Reservations.Dto;

public class CreateReservationApiDto
{
    public int ScreeningId { get; set; }
    public List<int> SeatIds { get; set; }
}