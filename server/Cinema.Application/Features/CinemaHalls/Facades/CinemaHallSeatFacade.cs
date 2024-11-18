using AutoMapper;
using Cinema.Application.Features.CinemaHalls.Dto;
using Cinema.Application.Features.CinemaHalls.Exceptions;
using Cinema.Application.Features.CinemaHalls.Interfaces;
using Cinema.Application.Features.Seats.Interfaces;

namespace Cinema.Application.Features.CinemaHalls.Facades;

internal class CinemaHallSeatFacade : ICinemaHallSeatFacade
{
    private readonly ICinemaHallService _cinemaHallService;
    private readonly IMapper _mapper;
    private readonly ISeatBuilder _seatBuilder;
    private readonly ISeatService _seatService;

    public CinemaHallSeatFacade(ICinemaHallService cinemaHallService, IMapper mapper,
        ISeatBuilder seatBuilder, ISeatService seatService
    )
    {
        _cinemaHallService = cinemaHallService;
        _mapper = mapper;
        _seatBuilder = seatBuilder;
        _seatService = seatService;
    }

    public async Task<CinemaHallSeatAppResponseDto> CreateAsync(int cinemaHallId,
        CreateCinemaHallSeatAppDto createSeatDto)
    {
        var existingCinemaHall = await _cinemaHallService.GetByIdWithDetailsAsync(cinemaHallId);

        if (!existingCinemaHall.CanAddSeats(1))
        {
            throw new CinemaHallCapacityExceededException(existingCinemaHall.Capacity, 1);
        }

        var createdSeat = CreateForValidatedCinemaHallAsync(cinemaHallId, createSeatDto);

        return _mapper.Map<CinemaHallSeatAppResponseDto>(createdSeat);
    }

    public async Task<CinemaHallSeatAppResponseDto> CreateForValidatedCinemaHallAsync(int cinemaHallId,
        CreateCinemaHallSeatAppDto createSeatDto)
    {
        var seat = _seatBuilder
            .SetColumn(createSeatDto.Column)
            .SetRow(createSeatDto.Row)
            .SetSeatType(createSeatDto.Type)
            .SetCinemaHallId(cinemaHallId)
            .Build();

        var createdSeat = await _seatService.CreateAsync(seat);

        return _mapper.Map<CinemaHallSeatAppResponseDto>(createdSeat);
    }

    public async Task<CinemaHallSeatAppResponseDto> UpdateAsync(int cinemaHallId, int seatId,
        UpdateCinemaHallSeatAppDto updateCinemaHallSeatDto)
    {
        var existingCinemaHall = await _cinemaHallService.GetByIdWithDetailsAsync(cinemaHallId);

        var seat = _seatBuilder
            .SetId(seatId)
            .SetColumn(updateCinemaHallSeatDto.Column)
            .SetRow(updateCinemaHallSeatDto.Row)
            .SetSeatType(updateCinemaHallSeatDto.Type)
            .SetCinemaHallId(existingCinemaHall.Id)
            .Build();

        var updatedSeat = await _seatService.UpdateAsync(seat);

        return _mapper.Map<CinemaHallSeatAppResponseDto>(updatedSeat);
    }

    public async Task DeleteAsync(int seatId)
    {
        await _seatService.DeleteAsync(seatId);
    }

    public async Task<CinemaHallSeatAppResponseDto> GetByIdAsync(int seatId)
    {
        var seat = await _seatService.GetByIdAsync(seatId);
        return _mapper.Map<CinemaHallSeatAppResponseDto>(seat);
    }
}