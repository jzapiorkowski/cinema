namespace Cinema.Application.Features.Reservations.Dto;

public class ReservationSeatAppResponseDto
{
    public int Id { get; set; }
    public int SeatId { get; set; }
    public ReservationTicketAppResponseDto? Ticket { get; set; }
}