using AutoMapper;
using Cinema.API.Features.Persons.Dto;
using Cinema.Application.Features.Persons.Dto;

namespace Cinema.API.Features.Persons.Mappers;

internal class PersonProfile : Profile
{
    public PersonProfile()
    {
        CreateMap<PersonAppResponseDto, PersonApiResponseDto>();
        CreateMap<PersonWithDetailsAppResponseDto, PersonWithDetailsApiResponseDto>();
        CreateMap<PersonActedInAppResponseDto, PersonActedInApiResponseDto>();
        CreateMap<PersonMovieAppResponseDto, PersonMovieApiResponseDto>();

        CreateMap<CreatePersonApiDto, CreatePersonAppDto>();
        CreateMap<CreatePersonActedInApiDto, CreatePersonActedInAppDto>();

        CreateMap<UpdatePersonApiDto, UpdatePersonAppDto>();
        CreateMap<UpdatePersonActedInApiDto, UpdatePersonActedInAppDto>();
    }
}