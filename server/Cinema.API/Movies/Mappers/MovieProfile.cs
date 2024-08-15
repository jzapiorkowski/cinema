using AutoMapper;
using Cinema.API.Movies.Dto;
using Cinema.Application.Movies.Dto;

namespace Cinema.API.Movies.Mappers;

internal class MovieProfile : Profile
{
    public MovieProfile()
    {
        CreateMap<MovieAppResponseDto, MovieApiResponseDto>();
        CreateMap<CreateMovieApiDto, CreateMovieAppDto>();
        CreateMap<UpdateMovieApiDto, UpdateMovieAppDto>();
    }
}
