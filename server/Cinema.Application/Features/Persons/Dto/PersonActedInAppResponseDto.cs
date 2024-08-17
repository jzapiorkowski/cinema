namespace Cinema.Application.Features.Persons.Dto;

public class PersonActedInAppResponseDto
{
    public PersonMovieAppResponseDto Movie { get; set; }
    public string Role { get; set; }
}