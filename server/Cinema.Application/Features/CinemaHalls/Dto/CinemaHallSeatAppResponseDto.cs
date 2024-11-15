using Cinema.Domain.Features.Seats.Entities;

namespace Cinema.Application.Features.CinemaHalls.Dto;

public class CinemaHallSeatAppResponseDto
{
    public int Id { get; set; }
    public int Row { get; set; }
    public int Column { get; set; }
    public SeatType Type { get; set; }
}