using AutoMapper;
using Cinema.Application.Features.Persons.Dto;
using Cinema.Domain.Core.Pagination;
using Cinema.Domain.Features.Movies.Entities;
using Cinema.Domain.Features.Persons.Entities;

namespace Cinema.Application.Features.Persons.Mappers;

internal class PersonProfile : Profile
{
    public PersonProfile()
    {
        CreateMap<Person, PersonAppResponseDto>();
        CreateMap<Movie, PersonMovieAppResponseDto>();
        
        CreateMap<PaginationResponse<Person>, PaginationResponse<PersonAppResponseDto>>()
            .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Data.Select(p => new PersonAppResponseDto
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                BirthDate = p.BirthDate
            })));

        CreateMap<Person, PersonWithDetailsAppResponseDto>()
            .ForMember(dest => dest.ActedIn, opt => opt.MapFrom((src, dest, destMember, context) =>
                src.MovieActors.Select(ma => new PersonActedInAppResponseDto
                {
                    Movie = context.Mapper.Map<PersonMovieAppResponseDto>(ma.Movie),
                    Role = ma.Role
                })
            ))
            .ForMember(dest => dest.DirectedMovies, opt => opt.MapFrom(src => src.DirectedMovies.Select(dm =>
                new MovieDirectedAppResponseDto
                {
                    Id = dm.Id,
                    Title = dm.Title,
                    Genre = dm.Genre,
                    ReleaseDate = dm.ReleaseDate
                }
            )));

        CreateMap<Movie, MovieDirectedAppResponseDto>();
    }
}