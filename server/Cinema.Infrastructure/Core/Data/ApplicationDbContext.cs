using Cinema.Domain.Core.Interfaces;
using Cinema.Domain.Features.Movies.Entities;
using Cinema.Domain.Features.Persons.Entities;
using Cinema.Infrastructure.Features.MovieActors.ModelConfigurations;
using Cinema.Infrastructure.Features.Movies.ModelConfigurations;
using Cinema.Infrastructure.Features.Persons.ModelConfigurations;
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

        // register relationships configurations
        modelBuilder.ApplyConfiguration(new MovieActorConfiguration());
    }

    public DbSet<Movie> Movie { get; set; }
    public DbSet<Person> Person { get; set; }
}