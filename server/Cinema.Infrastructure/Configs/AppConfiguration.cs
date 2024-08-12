using Cinema.Domain.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Cinema.Infrastructure.Configs;

public class AppConfiguration : IAppConfiguration
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