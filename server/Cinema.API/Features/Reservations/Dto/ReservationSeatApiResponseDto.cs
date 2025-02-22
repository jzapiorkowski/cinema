namespace Cinema.API.Features.Reservations.Dto;

public class ReservationSeatApiResponseDto
{
    public int Id { get; set; }
    public int SeatId { get; set; }
    public ReservationTicketApiResponseDto? Ticket { get; set; }
}