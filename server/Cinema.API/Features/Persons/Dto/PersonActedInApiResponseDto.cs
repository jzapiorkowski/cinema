namespace Cinema.API.Features.Persons.Dto;

public class PersonActedInApiResponseDto
{
    public PersonMovieApiResponseDto Movie { get; set; }
    public string Role { get; set; }
}