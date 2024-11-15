using AutoMapper;
using Cinema.Application.Features.CinemaHalls.Dto;
using Cinema.Application.Features.CinemaHalls.Exceptions;
using Cinema.Application.Features.CinemaHalls.Interfaces;
using Cinema.Application.Features.Seats.Interfaces;
using Cinema.Domain.Shared.Interfaces;

namespace Cinema.Application.Features.CinemaHalls.Facades;

internal class CinemaHallFacade : ICinemaHallFacade
{
    private readonly ICinemaHallService _cinemaHallService;
    private readonly IMapper _mapper;
    private readonly ICinemaHallBuilder _cinemaHallBuilder;
    private readonly ISeatBuilder _seatBuilder;
    private readonly ISeatService _seatService;
    private readonly ITransactionalUnitOfWork _transactionalUnitOfWork;

    public CinemaHallFacade(ICinemaHallService cinemaHallService, IMapper mapper, ICinemaHallBuilder cinemaHallBuilder,
        ISeatBuilder seatBuilder, ISeatService seatService, ITransactionalUnitOfWork transactionalUnitOfWork
    )
    {
        _cinemaHallService = cinemaHallService;
        _mapper = mapper;
        _cinemaHallBuilder = cinemaHallBuilder;
        _seatBuilder = seatBuilder;
        _seatService = seatService;
        _transactionalUnitOfWork = transactionalUnitOfWork;
    }

    public async Task<IEnumerable<CinemaHallAppResponseDto>> GetAllAsync()
    {
        var cinemaHalls = await _cinemaHallService.GetAllAsync();
        return _mapper.Map<IEnumerable<CinemaHallAppResponseDto>>(cinemaHalls);
    }

    public async Task<CinemaHallWithDetailsAppResponseDto> GetByIdWithDetailsAsync(int id)
    {
        var cinemaHall = await _cinemaHallService.GetByIdWithDetailsAsync(id);
        return _mapper.Map<CinemaHallWithDetailsAppResponseDto>(cinemaHall);
    }

    public async Task DeleteAsync(int id)
    {
        await _cinemaHallService.DeleteAsync(id);
    }

    public async Task<CinemaHallAppResponseDto> CreateAsync(CreateCinemaHallAppDto createCinemaHallAppDto)
    {
        await _transactionalUnitOfWork.BeginTransactionAsync();

        var cinemaHall = _cinemaHallBuilder
            .SetCinemaBuildingId(createCinemaHallAppDto.CinemaBuildingId)
            .SetNumber(createCinemaHallAppDto.Number)
            .SetCapacity(createCinemaHallAppDto.Capacity)
            .Build();

        if (!cinemaHall.CanAddSeats(createCinemaHallAppDto.Seats.Count))
        {
            throw new CinemaHallCapacityExceededException(cinemaHall.Capacity, createCinemaHallAppDto.Seats.Count);
        }

        var createdCinemaHall = await _cinemaHallService.CreateAsync(cinemaHall);

        foreach (var createSeatDto in createCinemaHallAppDto.Seats)
        {
            var seat = _seatBuilder
                .SetColumn(createSeatDto.Column)
                .SetRow(createSeatDto.Row)
                .SetSeatType(createSeatDto.Type)
                .SetCinemaHallId(createdCinemaHall.Id)
                .Build();

            await _seatService.CreateAsync(seat);
        }

        await _transactionalUnitOfWork.CommitTransactionAsync();

        return _mapper.Map<CinemaHallAppResponseDto>(createdCinemaHall);
    }
}