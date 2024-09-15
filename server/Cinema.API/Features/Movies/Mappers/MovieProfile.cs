using AutoMapper;
using Cinema.API.Features.Movies.Dto;
using Cinema.Application.Features.Movies.Dto;

namespace Cinema.API.Features.Movies.Mappers;

internal class MovieProfile : Profile
{
    public MovieProfile()
    {
        CreateMap<MovieAppResponseDto, MovieApiResponseDto>();
        CreateMap<MovieWithDetailsAppResponseDto, MovieWithDetailsApiResponseDto>();
        CreateMap<CreateMovieApiDto, CreateMovieAppDto>();
        CreateMap<UpdateMovieApiDto, UpdateMovieAppDto>();
        
        CreateMap<MovieActorAppResponseDto, MovieActorApiResponseDto>();
        CreateMap<MovieDirectorAppResponseDto, MovieDirectorApiResponseDto>();
        
        CreateMap<CreateMovieActorApiDto, CreateMovieActorAppDto>();
        CreateMap<UpdateMovieActorApiDto, UpdateMovieActorAppDto>();
    }
}
