using Cinema.Domain.Core.Interfaces;
using Cinema.Domain.Movies.Entities;
using Cinema.Infrastructure.Movies.ModelConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Infrastructure.Core.Data;

internal class ApplicationDbContext : DbContext
{
    private readonly IAppConfiguration _config;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IAppConfiguration config): base(options)
    {
        _config = config;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(_config.GetDbConnectionString());
    
    protected override void OnModelCreating(ModelBuilder modelBuilder) 
    {
        modelBuilder.ApplyConfiguration(new MovieConfiguration());
    }
    
    public DbSet<Movie> Movies { get; set; }
}