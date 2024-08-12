using AutoMapper;
using Cinema.API.Dto;
using Cinema.Application.Dto;

namespace Cinema.API.Mappers;

public class MovieProfile : Profile
{
    public MovieProfile()
    {
        CreateMap<MovieAppResponseDto, MovieApiResponseDto>();
        CreateMap<CreateMovieApiDto, CreateMovieAppDto>();
        CreateMap<UpdateMovieApiDto, UpdateMovieAppDto>();
    }
}
