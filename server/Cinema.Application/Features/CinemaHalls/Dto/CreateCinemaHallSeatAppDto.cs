using Cinema.Domain.Features.Seats.Entities;

namespace Cinema.Application.Features.CinemaHalls.Dto;

public class CreateCinemaHallSeatAppDto
{
    public int Row { get; set; }
    public int Column { get; set; }
    public SeatType Type { get; set; }
}