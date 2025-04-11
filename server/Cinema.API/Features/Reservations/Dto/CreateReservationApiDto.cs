using System.ComponentModel.DataAnnotations;

namespace Cinema.API.Features.Reservations.Dto;

public class CreateReservationApiDto
{
    [Required]
    public int ScreeningId { get; set; }
    
    [Required]
    [MinLength(0)]
    public List<int> SeatIds { get; set; }
    
    public Guid? CustomerId { get; set; }
}