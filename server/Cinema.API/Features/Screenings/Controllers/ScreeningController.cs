using AutoMapper;
using Cinema.API.Features.Screenings.Dto;
using Cinema.Application.Features.Screenings.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.API.Features.Screenings.Controllers;

[ApiController]
[Route("screenings")]
public class ScreeningController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IScreeningFacade _screeningFacade;
    
    public ScreeningController(IMapper mapper, IScreeningFacade screeningFacade)
    {
        _mapper = mapper;
        _screeningFacade = screeningFacade;
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType<ScreeningWithDetailsApiResponseDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetWithDetailsByIdAsync(int id)
    {
        var screening = await _screeningFacade.GetWithDetailsByIdAsync(id);
        return Ok(_mapper.Map<ScreeningWithDetailsApiResponseDto>(screening));
    }
}