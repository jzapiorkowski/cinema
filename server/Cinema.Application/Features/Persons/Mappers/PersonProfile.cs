using AutoMapper;
using Cinema.Application.Features.Persons.Dto;
using Cinema.Domain.Core.Pagination;
using Cinema.Domain.Features.MovieActors.Entities;
using Cinema.Domain.Features.Movies.Entities;
using Cinema.Domain.Features.Persons.Entities;

namespace Cinema.Application.Features.Persons.Mappers;

internal class PersonProfile : Profile
{
    public PersonProfile()
    {
        CreateMap<Person, PersonAppResponseDto>();

        CreateMap<PaginationResponse<Person>, PaginationResponse<PersonAppResponseDto>>();

        CreateMap<Person, PersonWithDetailsAppResponseDto>()
            .ForMember(dest => dest.ActedIn, opt => opt.MapFrom(src => src.MovieActors))
            .ForMember(dest => dest.DirectedMovies, opt => opt.MapFrom(src => src.DirectedMovies));
        CreateMap<MovieActor, PersonActedInAppResponseDto>();
        CreateMap<Movie, PersonMovieAppResponseDto>();
        CreateMap<Movie, MovieDirectedAppResponseDto>();
    }
}