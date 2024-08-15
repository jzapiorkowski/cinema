namespace Cinema.Domain.Core.Interfaces;

public interface IAppConfiguration
{
    string GetDbConnectionString();
}