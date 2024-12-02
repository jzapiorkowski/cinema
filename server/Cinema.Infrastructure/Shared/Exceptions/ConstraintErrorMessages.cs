using Cinema.Infrastructure.Shared.Constants;

namespace Cinema.Infrastructure.Shared.Exceptions;

internal static class ConstraintErrorMessages
{
    private static readonly Dictionary<string, string> ErrorMessages = new()
    {
        {
            DatabaseConstraints.UQSeatCinemaHallRowColumn,
            "A seat with the same row and column already exists in this cinema hall."
        },
        {
            DatabaseConstraints.UQCinemaHallCinemaBuildingIdNumber,
            "A cinema hall with the same number already exists in this cinema building."
        },
        {
            DatabaseConstraints.FKScreeningMovie,
            "This movie cannot be deleted because it has associated screenings."
        },
        {
            DatabaseConstraints.FKMovieDirector,
            "This director cannot be deleted because they have associated movies."
        },
        {
            DatabaseConstraints.FKCinemaHallCinemaBuilding,
            "This cinema building cannot be deleted because it has associated cinema halls."
        }
    };

    public static string GetErrorMessage(string constraintName) =>
        ErrorMessages.GetValueOrDefault(constraintName, "A database constraint violation occurred.");
}