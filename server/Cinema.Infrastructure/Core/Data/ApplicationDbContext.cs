using Cinema.Domain.Core.Interfaces;
using Cinema.Domain.Features.CinemaBuildings.Entities;
using Cinema.Domain.Features.CinemaHalls.Entities;
using Cinema.Domain.Features.Movies.Entities;
using Cinema.Domain.Features.Persons.Entities;
using Cinema.Domain.Features.Screenings.Entities;
using Cinema.Domain.Features.Seats.Entities;
using Cinema.Infrastructure.Features.CinemaBuildings.ModelConfigurations;
using Cinema.Infrastructure.Features.CinemaHalls.ModelConfigurations;
using Cinema.Infrastructure.Features.MovieActors.ModelConfigurations;
using Cinema.Infrastructure.Features.Movies.ModelConfigurations;
using Cinema.Infrastructure.Features.Persons.ModelConfigurations;
using Cinema.Infrastructure.Features.Screenings.ModelConfigurations;
using Cinema.Infrastructure.Features.Seats.ModelConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Infrastructure.Core.Data;

internal class ApplicationDbContext : DbContext
{
    private readonly IAppConfiguration _config;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IAppConfiguration config) :
        base(options)
    {
        _config = config;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_config.GetDbConnectionString());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // register model configurations
        modelBuilder.ApplyConfiguration(new MovieConfiguration());
        modelBuilder.ApplyConfiguration(new PersonConfiguration());
        modelBuilder.ApplyConfiguration(new ScreeningConfiguration());
        modelBuilder.ApplyConfiguration(new CinemaHallConfiguration());
        modelBuilder.ApplyConfiguration(new CinemaBuildingConfiguration());
        modelBuilder.ApplyConfiguration(new SeatConfiguration());

        // register relationships configurations
        modelBuilder.ApplyConfiguration(new MovieActorConfiguration());
    }

    public DbSet<Movie> Movie { get; set; }
    public DbSet<Person> Person { get; set; }
    public DbSet<Screening> Screening { get; set; }
    public DbSet<CinemaHall> CinemaHall { get; set; }
    public DbSet<CinemaBuilding> CinemaBuilding { get; set; }
    public DbSet<Seat> Seat { get; set; }
}