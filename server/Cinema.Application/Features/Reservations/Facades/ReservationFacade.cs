using AutoMapper;
using Cinema.Application.Features.Reservations.Dto;
using Cinema.Application.Features.Reservations.Interfaces;
using Cinema.Domain.Core.Pagination;
using Cinema.Domain.Shared.Interfaces;

namespace Cinema.Application.Features.Reservations.Facades;

internal class ReservationFacade : IReservationFacade
{
    private readonly IMapper _mapper;
    private readonly IReservationService _reservationService;
    private readonly IReservationBuilder _reservationBuilder;
    private readonly ITransactionalUnitOfWork _transactionalUnitOfWork;
    private readonly IReservationSeatBuilder _reservationSeatBuilder;

    public ReservationFacade(IMapper mapper, IReservationService reservationService,
        IReservationBuilder reservationBuilder, ITransactionalUnitOfWork transactionalUnitOfWork,
        IReservationSeatBuilder reservationSeatBuilder)
    {
        _mapper = mapper;
        _reservationService = reservationService;
        _reservationBuilder = reservationBuilder;
        _transactionalUnitOfWork = transactionalUnitOfWork;
        _reservationSeatBuilder = reservationSeatBuilder;
    }

    public async Task<ReservationAppResponseDto> CreateAsync(CreateReservationAppDto createReservationDto)
    {
        var reservation = _reservationBuilder
            .SetScreeningId(createReservationDto.ScreeningId)
            .Build();

        try
        {
            await _transactionalUnitOfWork.BeginTransactionAsync();
            var createdReservation = await _reservationService.CreateAsync(reservation);
            var reservationSeats = createReservationDto.SeatIds.Select(seatId =>
                    _reservationSeatBuilder
                        .SetReservationId(reservation.Id)
                        .SetSeatId(seatId)
                        .Build())
                .ToList();
            await _reservationService.AddSeatsToReservationAsync(createdReservation.ScreeningId, reservationSeats);
            await _transactionalUnitOfWork.CommitTransactionAsync();

            return await GetByIdAsync(createdReservation.Id);
        }
        catch
        {
            await _transactionalUnitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task<ReservationAppResponseDto> AddSeatToReservationAsync(int reservationId, int seatId)
    {
        var reservationSeat = _reservationSeatBuilder
            .SetReservationId(reservationId)
            .SetSeatId(seatId)
            .Build();
        var reservation = await _reservationService.GetByIdAsync(reservationId);
        await _reservationService.AddSeatsToReservationAsync(reservation.ScreeningId, [reservationSeat]);

        return await GetByIdAsync(reservationId);
    }

    public async Task<ReservationAppResponseDto> RemoveSeatFromReservationAsync(int reservationId, int seatId)
    {
        await _reservationService.RemoveSeatFromReservationAsync(reservationId, seatId);
        return await GetByIdAsync(reservationId);
    }

    public async Task<ReservationAppResponseDto> GetByIdAsync(int id)
    {
        var reservationEntity = await _reservationService.GetByIdAsync(id);
        return _mapper.Map<ReservationAppResponseDto>(reservationEntity);
    }

    public async Task<PaginationResponse<ReservationAppResponseDto>> GetAllAsync(PaginationRequest paginationRequest)
    {
        var paginationResponse = await _reservationService.GetAllAsync(paginationRequest);
        return _mapper.Map<PaginationResponse<ReservationAppResponseDto>>(paginationResponse);
    }

    public async Task<ReservationAppResponseDto> CancelReservationAsync(int id)
    {
        try
        {
            await _transactionalUnitOfWork.BeginTransactionAsync();
            var canceledReservation = await _reservationService.CancelReservationAsync(id);
            await _transactionalUnitOfWork.CommitTransactionAsync();
            return _mapper.Map<ReservationAppResponseDto>(canceledReservation);
        }
        catch
        {
            await _transactionalUnitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task<ReservationAppResponseDto> ConfirmReservationAsync(int id)
    {
        try
        {
            await _transactionalUnitOfWork.BeginTransactionAsync();
            var reservation = await _reservationService.ConfirmReservationAsync(id);
            await _transactionalUnitOfWork.CommitTransactionAsync();
            return _mapper.Map<ReservationAppResponseDto>(reservation);
        }
        catch
        {
            await _transactionalUnitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}