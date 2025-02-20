namespace Cinema.API.Features.Reservations.Dto;

public class ReservationScreeningApiResponseDto
{
    public int Id { get; set; }
    public DateTime StartTime { get; set; }
    public ReservationMovieApiResponseDto Movie { get; set; }
    public ReservationCinemaHallApiResponseDto CinemaHall { get; set; }
}