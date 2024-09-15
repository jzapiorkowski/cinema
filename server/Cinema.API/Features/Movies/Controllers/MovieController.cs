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
    private readonly IMovieFacade _movieFacade;
    private readonly IMapper _mapper;

    public MovieController(IMovieFacade movieFacade, IMapper mapper)
    {
        _movieFacade = movieFacade;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType<List<MovieApiResponseDto>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAsync()
    {
        var movies = await _movieFacade.GetAllAsync();
        return Ok(_mapper.Map<List<MovieApiResponseDto>>(movies));
    }

    [HttpGet("{movieId:int}")]
    [ProducesResponseType<MovieWithDetailsApiResponseDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdWithDetailsAsync(int movieId)
    {
        var movie = await _movieFacade.GetByIdAWithDetailsAsync(movieId);
        return Ok(_mapper.Map<MovieWithDetailsApiResponseDto>(movie));
    }

    [HttpPost]
    [ProducesResponseType<MovieApiResponseDto>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateMovieApiDto movie)
    {
        var createdMovie = await _movieFacade.CreateAsync(_mapper.Map<CreateMovieAppDto>(movie));
        var movieApiResponse = _mapper.Map<MovieApiResponseDto>(createdMovie);

        return CreatedAtAction(nameof(GetByIdWithDetailsAsync), new { movieId = movieApiResponse.Id },
            movieApiResponse);
    }

    [HttpDelete("{movieId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(int movieId)
    {
        await _movieFacade.DeleteAsync(movieId);
        return NoContent();
    }

    [HttpPut("{movieId:int}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType<MovieAppResponseDto>(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateAsync(int movieId, [FromBody] UpdateMovieApiDto movie)
    {
        var updatedMovie = await _movieFacade.UpdateAsync(movieId, _mapper.Map<UpdateMovieAppDto>(movie));
        return Ok(_mapper.Map<MovieAppResponseDto>(updatedMovie));
    }
}