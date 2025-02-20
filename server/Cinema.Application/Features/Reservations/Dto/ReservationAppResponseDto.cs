using Cinema.Domain.Features.Reservations.Entities;

namespace Cinema.Application.Features.Reservations.Dto;

public class ReservationAppResponseDto
{
    public int Id { get; set; }
    public ReservationScreeningAppResponseDto Screening { get; set; }
    public ReservationStatus Status { get; set; }
    public IEnumerable<ReservationSeatAppResponseDto> ReservationSeats { get; set; } = [];
}