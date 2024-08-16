using AutoMapper;
using Cinema.API.Features.Movies.Dto;
using Cinema.Application.Features.Movies.Dto;
using Cinema.Application.Features.Movies.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.API.Features.Movies.Controllers;

[ApiController]
[Route("movies")]
public class MovieController : ControllerBase
{
    private readonly IMovieService _movieService;
    private readonly IMapper _mapper;

    public MovieController(IMovieService movieService, IMapper mapper)
    {
        _movieService = movieService;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType<List<MovieApiResponseDto>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAsync()
    {
        var movies = await _movieService.GetAllAsync();
        return Ok(_mapper.Map<List<MovieApiResponseDto>>(movies));
    }

    [HttpGet("{movieId:int}")]
    [ProducesResponseType<MovieWithActorsApiResponseDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdWithDetailsAsync(int movieId)
    {
        var movie = await _movieService.GetByIdAsync(movieId);
        return Ok(_mapper.Map<MovieWithActorsApiResponseDto>(movie));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateMovieAsync([FromBody] CreateMovieApiDto movie)
    {
        await _movieService.CreateAsync(_mapper.Map<CreateMovieAppDto>(movie));
        return Ok();
    }

    [HttpDelete("{movieId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(int movieId)
    {
        await _movieService.DeleteAsync(movieId);
        return NoContent();
    }

    [HttpPut("{movieId:int}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateAsync(int movieId, [FromBody] UpdateMovieApiDto movie)
    {
        await _movieService.UpdateAsync(movieId, _mapper.Map<UpdateMovieAppDto>(movie));
        return NoContent();
    }
}