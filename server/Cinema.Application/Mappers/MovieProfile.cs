using AutoMapper;
using Cinema.Application.Dto;
using Cinema.Domain.Entities;

namespace Cinema.Application.Mappers;

public class MovieProfile : Profile
{
    public MovieProfile()
    {
        CreateMap<Movie, MovieAppResponseDto>();
        CreateMap<CreateMovieAppDto, Movie>();
        CreateMap<UpdateMovieAppDto, Movie>();
    }
}
