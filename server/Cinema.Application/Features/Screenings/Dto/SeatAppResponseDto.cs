using Cinema.Domain.Features.Seats.Entities;

namespace Cinema.Application.Features.Screenings.Dto;

public class SeatAppResponseDto
{
    public int Id { get; set; }
    public int Row { get; set; }
    public int Column { get; set; }
    public SeatType Type { get; set; }
}