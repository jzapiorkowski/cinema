using AutoMapper;
using Cinema.API.Dto;
using Cinema.Application.Dto;
using Cinema.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.API.Controllers;

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
    public async Task<IActionResult> GetAllMovies()
    {
        try
        {
            var movies = await _movieService.GetAllMovies();

            return Ok(_mapper.Map<List<MovieApiResponseDto>>(movies));
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return Problem();
        }
    }

    [HttpGet("{movieId:int}")]
    [ProducesResponseType<MovieApiResponseDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetMovieById(int movieId)
    {
        try
        {
            var movie = await _movieService.GetMovieById(movieId);

            if (movie == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<MovieApiResponseDto>(movie));
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return Problem();
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateMovie([FromBody] CreateMovieApiDto movie)
    {
        try
        {
            await _movieService.CreateMovie(_mapper.Map<CreateMovieAppDto>(movie));

            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return Problem();
        }
    }

    [HttpDelete("{movieId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteMovie(int movieId)
    {
        try
        {
            var result = await _movieService.DeleteMovie(movieId);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return Problem();
        }
    }

    [HttpPut("{movieId:int}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateMovie(int movieId, [FromBody] UpdateMovieApiDto movie)
    {
        try
        {
            var result = await _movieService.UpdateMovie(movieId, _mapper.Map<UpdateMovieAppDto>(movie));

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return Problem();
        }
    }
}
