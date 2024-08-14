using AutoMapper;
using Cinema.Application.Dto;
using Cinema.Application.Interfaces;
using Cinema.Domain.Entities;
using Cinema.Domain.Interfaces;

namespace Cinema.Application.Services;

internal class MovieService : IMovieService
{
    private readonly IMovieRepository _movieRepository;
    private readonly IMapper _mapper;

    public MovieService(IMovieRepository movieRepository, IMapper mapper)
    {
        _movieRepository = movieRepository;
        _mapper = mapper;
    }

    public async Task CreateMovie(CreateMovieAppDto movie)
    {
        var movieModel = _mapper.Map<Movie>(movie);

        await _movieRepository.CreateMovie(movieModel);
    }

    public async Task<MovieAppResponseDto?> GetMovieById(int movieId)
    {
        var movie = await _movieRepository.GetMovieById(movieId);

        return _mapper.Map<MovieAppResponseDto>(movie);
    }

    public async Task<List<MovieAppResponseDto>> GetAllMovies()
    {
        var movies = await _movieRepository.GetAllMovies();

        return _mapper.Map<List<MovieAppResponseDto>>(movies);
    }

    public async Task<bool> DeleteMovie(int movieId)
    {
        return await _movieRepository.DeleteMovie(movieId);
    }

    public async Task<bool> UpdateMovie(int movieId, UpdateMovieAppDto movieDto)
    {
        var existingMovie = await _movieRepository.GetMovieById(movieId);

        if (existingMovie == null)
        {
            return false;
        }

        var updatedMovie = _mapper.Map(movieDto, existingMovie);

        await _movieRepository.UpdateMovie(updatedMovie);

        return true;
    }
}