using System.ComponentModel.DataAnnotations;

namespace Cinema.API.Features.CinemaHalls.Dto;

public class CreateCinemaHallApiDto
{
    [Required]
    public int CinemaBuildingId { get; set; }
    
    [Required]
    [Range(1, int.MaxValue)]
    public int Number { get; set; }
    
    [Required]
    [Range(1, int.MaxValue)]
    public int Capacity { get; set; }
    
    [Required]
    public List<CreateCinemaHallSeatApiDto> Seats { get; set; }
}