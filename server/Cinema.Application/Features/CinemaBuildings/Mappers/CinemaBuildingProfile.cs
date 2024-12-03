using AutoMapper;
using Cinema.Application.Features.CinemaBuildings.Dto;
using Cinema.Domain.Core.Pagination;
using Cinema.Domain.Features.CinemaBuildings.Entities;
using Cinema.Domain.Features.CinemaHalls.Entities;

namespace Cinema.Application.Features.CinemaBuildings.Mappers;

public class CinemaBuildingProfile : Profile
{
    public CinemaBuildingProfile()
    {
        CreateMap<CinemaBuilding, CinemaBuildingWithDetailsAppResponseDto>();
        CreateMap<CinemaBuilding, CinemaBuildingAppResponseDto>();
        
        CreateMap<PaginationResponse<CinemaBuilding>, PaginationResponse<CinemaBuildingAppResponseDto>>()
            .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Data.Select(cb => new CinemaBuildingAppResponseDto
            {
                Id = cb.Id,
                Address = cb.Address
            })));


        CreateMap<CinemaHall, CinemaBuildingHallAppResponseDto>();
    }
}