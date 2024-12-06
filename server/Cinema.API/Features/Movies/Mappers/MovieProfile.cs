using AutoMapper;
using Cinema.API.Features.Movies.Dto;
using Cinema.Application.Features.Movies.Dto;
using Cinema.Domain.Core.Pagination;

namespace Cinema.API.Features.Movies.Mappers;

internal class MovieProfile : Profile
{
    public MovieProfile()
    {
        CreateMap<MovieAppResponseDto, MovieApiResponseDto>();
        CreateMap<PaginationResponse<MovieAppResponseDto>, PaginationResponse<MovieApiResponseDto>>();

        CreateMap<MovieWithDetailsAppResponseDto, MovieWithDetailsApiResponseDto>();
        CreateMap<MovieActorAppResponseDto, MovieActorApiResponseDto>();
        CreateMap<MovieDirectorAppResponseDto, MovieDirectorApiResponseDto>();

        CreateMap<CreateMovieApiDto, CreateMovieAppDto>();
        CreateMap<CreateMovieActorApiDto, CreateMovieActorAppDto>();
        
        CreateMap<UpdateMovieApiDto, UpdateMovieAppDto>();
        CreateMap<UpdateMovieActorApiDto, UpdateMovieActorAppDto>();
    }
}