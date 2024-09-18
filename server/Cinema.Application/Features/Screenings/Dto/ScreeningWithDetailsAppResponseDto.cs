namespace Cinema.Application.Features.Screenings.Dto;

public class ScreeningWithDetailsAppResponseDto
{
    public int Id { get; set; }
    public DateTime StartTime { get; set; }
    public ScreeningMovieAppResponseDto Movie { get; set; }
}