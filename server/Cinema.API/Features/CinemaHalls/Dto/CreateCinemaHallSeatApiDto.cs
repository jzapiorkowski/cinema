using System.ComponentModel.DataAnnotations;
using Cinema.Domain.Features.Seats.Entities;

namespace Cinema.API.Features.CinemaHalls.Dto;

public class CreateCinemaHallSeatApiDto
{
    [Required]
    [Range(1, int.MaxValue)]
    public int Row { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int Column { get; set; }

    [Required]
    [EnumDataType(typeof(SeatType), ErrorMessage = "Invalid seat type")]
    public SeatType Type { get; set; }
}