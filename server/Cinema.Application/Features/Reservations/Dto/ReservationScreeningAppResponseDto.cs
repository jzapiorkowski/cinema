namespace Cinema.Application.Features.Reservations.Dto;

public class ReservationScreeningAppResponseDto
{
    public int Id { get; set; }
    public DateTime StartTime { get; set; }
    public ReservationMovieAppResponseDto Movie { get; set; }
    public ReservationCinemaHallAppResponseDto CinemaHall { get; set; }
}