using Cinema.Domain.Features.Reservations.Entities;

namespace Cinema.API.Features.Reservations.Dto;

public class ReservationApiResponseDto
{
    public int Id { get; set; }
    public ReservationScreeningApiResponseDto Screening { get; set; }
    public ReservationStatus Status { get; set; }
    public IEnumerable<ReservationSeatApiResponseDto> ReservationSeats { get; set; } = [];
}