namespace Cinema.API.Features.Screenings.Dto;

public class ScreeningWithDetailsApiResponseDto
{
    public int Id { get; set; }
    public DateTime StartTime { get; set; }
    public ScreeningMovieApiResponseDto Movie { get; set; }
}