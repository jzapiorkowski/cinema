namespace Cinema.Domain.Interfaces;

public interface IAppConfiguration
{
    string GetDbConnectionString();
}