namespace Cinema.Application.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string entityName, int id) : base($"Entity {entityName} with id {id} was not found.")
    {
    }
}