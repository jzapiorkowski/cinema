using AutoMapper;
using Cinema.Application.Movies.Dto;
using Cinema.Domain.Movies.Entities;
using Cinema.Domain.Persons.Entities;

namespace Cinema.Application.Movies.Mappers;

internal class MovieProfile : Profile
{
    public MovieProfile()
    {
        CreateMap<Movie, MovieAppResponseDto>();
        CreateMap<Movie, MovieWithActorAppResponseDto>()
            .ForMember(dest => dest.Actors, opt => opt.MapFrom(src => src.MovieActors.Select(ma =>
                new MovieActorAppResponseDto
                {
                    Id = ma.Actor.Id,
                    FirstName = ma.Actor.FirstName,
                    LastName = ma.Actor.LastName,
                    BirthDate = ma.Actor.BirthDate,
                    Role = ma.Role
                }
            )));
        CreateMap<CreateMovieAppDto, Movie>();
        CreateMap<UpdateMovieAppDto, Movie>();

        CreateMap<Person, MovieActorAppResponseDto>();
    }
}