using Cinema.Domain.Interfaces;
using Cinema.Infrastructure.Configs;
using Cinema.Infrastructure.Data;
using Cinema.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Cinema.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructureServices(this IServiceCollection serviceCollection)
    {
        // register db context
        serviceCollection.AddDbContext<ApplicationDbContext>();

        // register unit of work
        serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
        serviceCollection.AddScoped<ITransactionalUnitOfWork, TransactionalUnitOfWork>();
        
        // register configurations
        serviceCollection.AddSingleton<IAppConfiguration, AppConfiguration>();

        // register repositories
        serviceCollection.AddScoped<IMovieRepository, MovieRepository>();
    }
}