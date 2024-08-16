namespace Cinema.Application.Shared.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string entityName, int id) : base($"Entity {entityName} with id {id} was not found.")
    {
    }

    public NotFoundException(string entityName, ICollection<int> ids) : base(
        $"Entities {entityName} with ids {string.Join(", ", ids)} were not found.")
    {
    }
}