namespace Cinema.Domain.Shared.Exceptions;

public class InvalidSortByException : Exception
{
    public InvalidSortByException(string sortBy)
        : base($"Invalid SortBy property: {sortBy}")
    {
    }
}