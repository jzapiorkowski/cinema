using AutoMapper;
using Cinema.API.Features.Persons.Dto;
using Cinema.Application.Features.Persons.Dto;
using Cinema.Domain.Core.Pagination;

namespace Cinema.API.Features.Persons.Mappers;

internal class PersonProfile : Profile
{
    public PersonProfile()
    {
        CreateMap<PersonAppResponseDto, PersonApiResponseDto>();
        CreateMap<PaginationResponse<PersonAppResponseDto>, PaginationResponse<PersonApiResponseDto>>()
            .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Data));

        CreateMap<PersonWithDetailsAppResponseDto, PersonWithDetailsApiResponseDto>()
            .ForMember(dest => dest.DirectedMovies, opt => opt.MapFrom(src => src.DirectedMovies.Select(dm => dm)));

        CreateMap<PersonActedInAppResponseDto, PersonActedInApiResponseDto>();
        CreateMap<PersonMovieAppResponseDto, PersonMovieApiResponseDto>();

        CreateMap<CreatePersonApiDto, CreatePersonAppDto>();
        CreateMap<CreatePersonActedInApiDto, CreatePersonActedInAppDto>();

        CreateMap<UpdatePersonApiDto, UpdatePersonAppDto>();
        CreateMap<UpdatePersonActedInApiDto, UpdatePersonActedInAppDto>();

        CreateMap<MovieDirectedAppResponseDto, MovieDirectedApiResponseDto>();
    }
}