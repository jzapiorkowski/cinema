using Cinema.Domain.Core.Interfaces;
using Cinema.Domain.Movies.Interfaces;
using Cinema.Domain.Shared.Interfaces;
using Cinema.Infrastructure.Core.Configs;
using Cinema.Infrastructure.Core.Data;
using Cinema.Infrastructure.Movies.Repositories;
using Cinema.Infrastructure.Shared.UnitOfWork;
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