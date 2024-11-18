using AutoMapper;
using Cinema.Application.Features.CinemaHalls.Dto;
using Cinema.Application.Features.CinemaHalls.Exceptions;
using Cinema.Application.Features.CinemaHalls.Interfaces;
using Cinema.Domain.Shared.Interfaces;

namespace Cinema.Application.Features.CinemaHalls.Facades;

internal class CinemaHallFacade : ICinemaHallFacade
{
    private readonly ICinemaHallService _cinemaHallService;
    private readonly IMapper _mapper;
    private readonly ICinemaHallBuilder _cinemaHallBuilder;
    private readonly ITransactionalUnitOfWork _transactionalUnitOfWork;
    private readonly ICinemaHallSeatFacade _cinemaHallSeatFacade;

    public CinemaHallFacade(ICinemaHallService cinemaHallService, IMapper mapper, ICinemaHallBuilder cinemaHallBuilder,
        ITransactionalUnitOfWork transactionalUnitOfWork, ICinemaHallSeatFacade cinemaHallSeatFacade
    )
    {
        _cinemaHallService = cinemaHallService;
        _mapper = mapper;
        _cinemaHallBuilder = cinemaHallBuilder;
        _transactionalUnitOfWork = transactionalUnitOfWork;
        _cinemaHallSeatFacade = cinemaHallSeatFacade;
    }

    public async Task<IEnumerable<CinemaHallAppResponseDto>> GetAllAsync()
    {
        var cinemaHalls = await _cinemaHallService.GetAllAsync();
        return _mapper.Map<IEnumerable<CinemaHallAppResponseDto>>(cinemaHalls);
    }

    public async Task<CinemaHallAppResponseDto> UpdateAsync(int cinemaHallId,
        UpdateCinemaHallAppDto updateCinemaHallDto)
    {
        var existingCinemaHall = await _cinemaHallService.GetByIdAsync(cinemaHallId);

        var cinemaHall = _cinemaHallBuilder
            .SetId(cinemaHallId)
            .SetCinemaBuildingId(updateCinemaHallDto.CinemaBuildingId)
            .SetNumber(updateCinemaHallDto.Number)
            .SetCapacity(updateCinemaHallDto.Capacity)
            .Build();

        if (!existingCinemaHall.CanChangeCapacity(updateCinemaHallDto.Capacity))
        {
            throw new InvalidCinemaHallCapacityException(updateCinemaHallDto.Capacity);
        }

        var updatedCinemaHall = await _cinemaHallService.UpdateAsync(cinemaHall);

        return _mapper.Map<CinemaHallAppResponseDto>(updatedCinemaHall);
    }

    public async Task<CinemaHallWithDetailsAppResponseDto> GetByIdAsync(int id)
    {
        var cinemaHall = await _cinemaHallService.GetByIdAsync(id);
        return _mapper.Map<CinemaHallWithDetailsAppResponseDto>(cinemaHall);
    }

    public async Task DeleteAsync(int id)
    {
        await _cinemaHallService.DeleteAsync(id);
    }

    public async Task<CinemaHallAppResponseDto> CreateAsync(CreateCinemaHallAppDto createCinemaHallAppDto)
    {
        var cinemaHall = _cinemaHallBuilder
            .SetCinemaBuildingId(createCinemaHallAppDto.CinemaBuildingId)
            .SetNumber(createCinemaHallAppDto.Number)
            .SetCapacity(createCinemaHallAppDto.Capacity)
            .Build();

        if (!cinemaHall.CanAddSeats(createCinemaHallAppDto.Seats.Count))
        {
            throw new CinemaHallCapacityExceededException(cinemaHall.Capacity, createCinemaHallAppDto.Seats.Count);
        }
        
        await _transactionalUnitOfWork.BeginTransactionAsync();
        var createdCinemaHall = await _cinemaHallService.CreateAsync(cinemaHall);

        foreach (var createSeatDto in createCinemaHallAppDto.Seats)
        {
            await _cinemaHallSeatFacade.CreateForValidatedCinemaHallAsync(createdCinemaHall.Id, createSeatDto);
        }

        await _transactionalUnitOfWork.CommitTransactionAsync();

        return _mapper.Map<CinemaHallAppResponseDto>(createdCinemaHall);
    }
}