using Cinema.Domain.Features.Seats.Entities;

namespace Cinema.API.Features.Screenings.Dto;

public class SeatApiResponseDto
{
    public int Id { get; set; }
    public int Row { get; set; }
    public int Column { get; set; }
    public SeatType Type { get; set; }
}