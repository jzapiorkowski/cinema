using Cinema.Domain.Entities;
using Cinema.Domain.Interfaces;
using Cinema.Infrastructure.ModelConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Infrastructure.Data;

public class ApplicationDbContext : DbContext
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