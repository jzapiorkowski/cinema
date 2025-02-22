using AutoMapper;
using Cinema.API.Features.Reservations.Dto;
using Cinema.Application.Features.Reservations.Dto;
using Cinema.Domain.Core.Pagination;

namespace Cinema.API.Features.Reservations.Mappers;

public class ReservationProfile : Profile
{
    public ReservationProfile()
    {
        CreateMap<CreateReservationApiDto, CreateReservationAppDto>();

        CreateMap<PaginationResponse<ReservationAppResponseDto>, PaginationResponse<ReservationApiResponseDto>>();
        
        CreateMap<ReservationAppResponseDto, ReservationApiResponseDto>();
        CreateMap<ReservationScreeningAppResponseDto, ReservationScreeningApiResponseDto>();
        CreateMap<ReservationMovieAppResponseDto, ReservationMovieApiResponseDto>();
        CreateMap<ReservationSeatAppResponseDto, ReservationSeatApiResponseDto>();
        CreateMap<ReservationCinemaHallAppResponseDto, ReservationCinemaHallApiResponseDto>();
        CreateMap<ReservationTicketAppResponseDto, ReservationTicketApiResponseDto>();
    }
}