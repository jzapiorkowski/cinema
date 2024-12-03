using AutoMapper;
using Cinema.API.Features.CinemaBuildings.Dto;
using Cinema.Application.Features.CinemaBuildings.Dto;
using Cinema.Domain.Core.Pagination;

namespace Cinema.API.Features.CinemaBuildings.Mappers;

public class CinemaBuildingProfile : Profile
{
    public CinemaBuildingProfile()
    {
        CreateMap<CinemaBuildingAppResponseDto, CinemaBuildingApiResponseDto>();
        CreateMap<PaginationResponse<CinemaBuildingAppResponseDto>, PaginationResponse<CinemaBuildingApiResponseDto>>()
            .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Data));
        
        CreateMap<CinemaBuildingWithDetailsAppResponseDto, CinemaBuildingWithDetailsApiResponseDto>();

        CreateMap<CreateCinemaBuildingApiDto, CreateCinemaBuildingAppDto>();

        CreateMap<CinemaBuildingHallAppResponseDto, CinemaBuildingHallApiResponseDto>();
    }
}