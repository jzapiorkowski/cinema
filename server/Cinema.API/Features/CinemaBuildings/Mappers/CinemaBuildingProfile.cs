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
        CreateMap<PaginationResponse<CinemaBuildingAppResponseDto>, PaginationResponse<CinemaBuildingApiResponseDto>>();
        
        CreateMap<CinemaBuildingWithDetailsAppResponseDto, CinemaBuildingWithDetailsApiResponseDto>();
        CreateMap<CinemaBuildingHallAppResponseDto, CinemaBuildingHallApiResponseDto>();

        CreateMap<CreateCinemaBuildingApiDto, CreateCinemaBuildingAppDto>();
    }
}