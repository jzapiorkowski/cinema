using AutoMapper;
using Cinema.Application.Features.Persons.Dto;
using Cinema.Domain.Features.Movies.Entities;
using Cinema.Domain.Features.Persons.Entities;

namespace Cinema.Application.Features.Persons.Mappers;

internal class PersonProfile : Profile
{
    public PersonProfile()
    {
        CreateMap<Person, PersonAppResponseDto>();
        CreateMap<Movie, PersonMovieAppResponseDto>();

        CreateMap<Person, PersonWithDetailsAppResponseDto>()
            .ForMember(dest => dest.actedIn, opt => opt.MapFrom((src, dest, destMember, context) =>
                src.MovieActors.Select(ma => new PersonActedInAppResponseDto
                {
                    Movie = context.Mapper.Map<PersonMovieAppResponseDto>(ma.Movie),
                    Role = ma.Role
                })
            ));
    }
}