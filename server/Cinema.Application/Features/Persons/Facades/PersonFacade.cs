using AutoMapper;
using Cinema.Application.Features.Movies.Interfaces;
using Cinema.Application.Features.Persons.Dto;
using Cinema.Application.Features.Persons.Interfaces;
using Cinema.Domain.Features.MovieActors.Entities;

namespace Cinema.Application.Features.Persons.Facades;

internal class PersonFacade : IPersonFacade
{
    private readonly IPersonService _personService;
    private readonly IMapper _mapper;
    private readonly IPersonBuilder _personBuilder;
    private readonly IMovieRelatedEntityValidator _movieRelatedEntityValidator;

    public PersonFacade(IPersonService personService, IMapper mapper, IPersonBuilder personBuilder,
        IMovieRelatedEntityValidator movieRelatedEntityValidator)
    {
        _personService = personService;
        _mapper = mapper;
        _personBuilder = personBuilder;
        _movieRelatedEntityValidator = movieRelatedEntityValidator;
    }

    public async Task<IEnumerable<PersonAppResponseDto>> GetAllAsync()
    {
        var persons = await _personService.GetAllAsync();
        return _mapper.Map<IEnumerable<PersonAppResponseDto>>(persons);
    }

    public async Task<PersonWithDetailsAppResponseDto> GetByIdAsync(int id)
    {
        var person = await _personService.GetByIdAsync(id);
        return _mapper.Map<PersonWithDetailsAppResponseDto>(person);
    }

    public async Task DeleteAsync(int id)
    {
        await _personService.DeleteAsync(id);
    }

    public async Task<PersonAppResponseDto> CreateAsync(CreatePersonAppDto createPersonAppDto)
    {
        await _movieRelatedEntityValidator.ValidateEntitiesAsync(
            createPersonAppDto.ActedIn.Select(actedIn => actedIn.MovieId).ToList());
        var directedMovies = await _movieRelatedEntityValidator.ValidateEntitiesAsync(
            createPersonAppDto.DirectedMovies.ToList());

        var person = _personBuilder
            .SetFirstName(createPersonAppDto.FirstName)
            .SetLastName(createPersonAppDto.LastName)
            .SetBirthDate(createPersonAppDto.BirthDate)
            .SetMovieActors(createPersonAppDto.ActedIn.Select(actedIn =>
                    new MovieActor { MovieId = actedIn.MovieId, Role = actedIn.Role })
                .ToList()
            )
            .SetDirectedMovies(directedMovies)
            .Build();

        var createdPerson = await _personService.CreateAsync(person);
        return _mapper.Map<PersonAppResponseDto>(createdPerson);
    }

    public async Task<PersonAppResponseDto> UpdateAsync(int id, UpdatePersonAppDto updatePersonAppDto)
    {
        var existingPerson = await _personService.GetByIdAsync(id);

        await _movieRelatedEntityValidator.ValidateEntitiesAsync(
            updatePersonAppDto.ActedIn.Select(actedIn => actedIn.MovieId).ToList());
        var directedMovies = await _movieRelatedEntityValidator.ValidateEntitiesAsync(
            updatePersonAppDto.DirectedMovies.ToList());
        
        var person = _personBuilder
            .SetId(existingPerson.Id)
            .SetFirstName(updatePersonAppDto.FirstName)
            .SetLastName(updatePersonAppDto.LastName)
            .SetBirthDate(updatePersonAppDto.BirthDate)
            .SetMovieActors(updatePersonAppDto.ActedIn.Select(actedIn =>
                    new MovieActor { MovieId = actedIn.MovieId, Role = actedIn.Role })
                .ToList()
            )
            .SetDirectedMovies(directedMovies)
            .Build();

        var updatedPerson = await _personService.UpdateAsync(person);
        return _mapper.Map<PersonAppResponseDto>(updatedPerson);
    }
}