using Cinema.Domain.Movies.Entities;
using Cinema.Domain.Shared.Interfaces;

namespace Cinema.Domain.Movies.Interfaces;

public interface IMovieRepository : IBaseRepository<Movie>;