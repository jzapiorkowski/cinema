namespace Cinema.Application.Features.Reservations.Dto;

public class CreateReservationAppDto
{
    public int ScreeningId { get; set; }
    public List<int> SeatIds { get; set; }
}