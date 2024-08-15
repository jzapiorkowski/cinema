using Cinema.Domain.Core.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Cinema.Infrastructure.Core.Configs;

internal class AppConfiguration : IAppConfiguration
{
    private readonly IConfiguration _configuration;
    
    public AppConfiguration(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GetDbConnectionString()
    {
        return _configuration.GetConnectionString("DatabaseConnectionString");
    }
}