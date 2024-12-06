using AutoMapper;
using Cinema.Application.Features.Movies.Dto;
using Cinema.Domain.Core.Pagination;
using Cinema.Domain.Features.MovieActors.Entities;
using Cinema.Domain.Features.Movies.Entities;
using Cinema.Domain.Features.Persons.Entities;

namespace Cinema.Application.Features.Movies.Mappers;

internal class MovieProfile : Profile
{
    public MovieProfile()
    {
        CreateMap<Movie, MovieAppResponseDto>();
        CreateMap<PaginationResponse<Movie>, PaginationResponse<MovieAppResponseDto>>();

        CreateMap<Movie, MovieWithDetailsAppResponseDto>()
            .ForMember(dest => dest.Director, opt => opt.MapFrom(src => src.DirectedBy))
            .ForMember(dest => dest.Actors, opt => opt.MapFrom(src => src.MovieActors));
        CreateMap<Person, MovieDirectorAppResponseDto>();
        CreateMap<Person, MovieActorAppResponseDto>();
        CreateMap<MovieActor, MovieActorAppResponseDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ActorId))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Actor.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Actor.LastName))
            .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.Actor.BirthDate));

        CreateMap<CreateMovieAppDto, Movie>();
        CreateMap<UpdateMovieAppDto, Movie>();
    }
}