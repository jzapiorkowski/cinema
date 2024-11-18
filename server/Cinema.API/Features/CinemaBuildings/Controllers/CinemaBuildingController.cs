using AutoMapper;
using Cinema.API.Features.CinemaBuildings.Dto;
using Cinema.Application.Features.CinemaBuildings.Dto;
using Cinema.Application.Features.CinemaBuildings.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.API.Features.CinemaBuildings.Controllers;

[ApiController]
[Route("cinema-buildings")]
[Produces("application/json")]
public class CinemaBuildingController : ControllerBase
{
    private readonly ICinemaBuildingFacade _cinemaBuildingFacade;
    private readonly IMapper _mapper;

    public CinemaBuildingController(ICinemaBuildingFacade cinemaBuildingFacade, IMapper mapper)
    {
        _cinemaBuildingFacade = cinemaBuildingFacade;
        _mapper = mapper;
    }
    
    [HttpGet]
    [ProducesResponseType<List<CinemaBuildingApiResponseDto>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAsync()
    {
        var cinemaBuildings = await _cinemaBuildingFacade.GetAllAsync();
        return Ok(_mapper.Map<List<CinemaBuildingApiResponseDto>>(cinemaBuildings));
    }
    
    [HttpGet("{id:int}")]
    [ProducesResponseType<CinemaBuildingWithDetailsApiResponseDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var cinemaBuilding = await _cinemaBuildingFacade.GetByIdAsync(id);
        return Ok(_mapper.Map<CinemaBuildingWithDetailsApiResponseDto>(cinemaBuilding));
    }
    
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _cinemaBuildingFacade.DeleteAsync(id);
        return NoContent();
    }
    
    [HttpPost]
    [ProducesResponseType<CinemaBuildingApiResponseDto>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateCinemaBuildingApiDto createCinemaBuildingApiDto)
    {
        var createCinemaBuildingAppDto = _mapper.Map<CreateCinemaBuildingAppDto>(createCinemaBuildingApiDto);
        var createdCinemaBuilding = await _cinemaBuildingFacade.CreateAsync(createCinemaBuildingAppDto);
        return CreatedAtAction(nameof(GetByIdAsync), new { id = createdCinemaBuilding.Id },
            _mapper.Map<CinemaBuildingApiResponseDto>(createdCinemaBuilding));
    }
}