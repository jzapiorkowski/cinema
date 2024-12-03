using AutoMapper;
using Cinema.Application.Features.Movies.Dto;
using Cinema.Domain.Core.Pagination;
using Cinema.Domain.Features.Movies.Entities;
using Cinema.Domain.Features.Persons.Entities;

namespace Cinema.Application.Features.Movies.Mappers;

internal class MovieProfile : Profile
{
    public MovieProfile()
    {
        CreateMap<Movie, MovieAppResponseDto>();

        CreateMap<PaginationResponse<Movie>, PaginationResponse<MovieAppResponseDto>>()
            .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Data.Select(m => new MovieAppResponseDto
            {
                Id = m.Id,
                Title = m.Title,
                Genre = m.Genre,
                ReleaseDate = m.ReleaseDate,
                Duration = m.Duration
            })));
        
        CreateMap<Movie, MovieWithDetailsAppResponseDto>()
            .ForMember(dest => dest.Director, opt => opt.MapFrom(src => new MovieDirectorAppResponseDto
            {
                Id = src.DirectedBy.Id,
                FirstName = src.DirectedBy.FirstName,
                LastName = src.DirectedBy.LastName,
                BirthDate = src.DirectedBy.BirthDate
            }))
            .ForMember(dest => dest.Actors, opt => opt.MapFrom(src => src.MovieActors.Select(ma =>
                new MovieActorAppResponseDto
                {
                    Id = ma.Actor.Id,
                    FirstName = ma.Actor.FirstName,
                    LastName = ma.Actor.LastName,
                    BirthDate = ma.Actor.BirthDate,
                    Role = ma.Role
                }
            )))
            .ForMember(dest => dest.Director, opt => opt.MapFrom(src => src.DirectedBy));
        CreateMap<CreateMovieAppDto, Movie>();
        CreateMap<UpdateMovieAppDto, Movie>();

        CreateMap<Person, MovieActorAppResponseDto>();
        CreateMap<Person, MovieDirectorAppResponseDto>();
    }
}