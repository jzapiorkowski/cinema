using AutoMapper;
using Cinema.Application.Movies.Dto;
using Cinema.Domain.Movies.Entities;

namespace Cinema.Application.Movies.Mappers;

internal class MovieProfile : Profile
{
    public MovieProfile()
    {
        CreateMap<Movie, MovieAppResponseDto>();
        CreateMap<CreateMovieAppDto, Movie>();
        CreateMap<UpdateMovieAppDto, Movie>();
    }
}
