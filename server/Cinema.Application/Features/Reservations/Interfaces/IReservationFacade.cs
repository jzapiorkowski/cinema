using Cinema.Application.Features.Reservations.Dto;
using Cinema.Domain.Core.Pagination;

namespace Cinema.Application.Features.Reservations.Interfaces;

public interface IReservationFacade
{
    Task<ReservationAppResponseDto> CreateAsync(CreateReservationAppDto reservation);
    Task<ReservationAppResponseDto> AddSeatToReservationAsync(int reservationId, int seatId);
    Task<ReservationAppResponseDto> RemoveSeatFromReservationAsync(int reservationId, int seatId);
    Task<ReservationAppResponseDto> GetByIdAsync(int id);
    Task<PaginationResponse<ReservationAppResponseDto>> GetAllAsync(PaginationRequest paginationRequest);
}