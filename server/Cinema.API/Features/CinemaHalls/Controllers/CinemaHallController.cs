using AutoMapper;
using Cinema.API.Features.CinemaHalls.Dto;
using Cinema.Application.Features.CinemaHalls.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.API.Features.CinemaHalls.Controllers;

[ApiController]
[Route("cinema-halls")]
[Produces("application/json")]
public class CinemaHallController : ControllerBase
{
    private readonly ICinemaHallFacade _cinemaHallFacade;
    private readonly IMapper _mapper;

    public CinemaHallController(ICinemaHallFacade cinemaHallFacade, IMapper mapper)
    {
        _cinemaHallFacade = cinemaHallFacade;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType<List<CinemaHallApiResponseDto>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAsync()
    {
        var cinemaHalls = await _cinemaHallFacade.GetAllAsync();
        return Ok(_mapper.Map<List<CinemaHallApiResponseDto>>(cinemaHalls));
    }

    [HttpGet("{cinemaHallId:int}")]
    [ProducesResponseType<CinemaHallApiResponseDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(int cinemaHallId)
    {
        var cinemaHall = await _cinemaHallFacade.GetByIdAWithDetailsAsync(cinemaHallId);
        return Ok(_mapper.Map<CinemaHallApiResponseDto>(cinemaHall));
    }
}