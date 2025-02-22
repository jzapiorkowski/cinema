namespace Cinema.Infrastructure.Shared.Constants;

internal static class DatabaseConstraints
{
    public const string FKCinemaHallCinemaBuilding = "FK_cinema_hall_cinema_building_id";
    public const string FKMovieActorMovie = "FK_movie_actor_movie_id";
    public const string FKMovieActorActor = "FK_movie_actor_actor_id";
    public const string FKMovieDirector = "FK_movie_director_id";
    public const string FKScreeningMovie = "FK_screening_movie_id";
    public const string FKTicketReservationSeat = "FK_ticket_reservation_seat_id";
    public const string FKScreeningReservation = "FK_screening_reservation_id";
    public const string FKSeatReservationSeat = "FK_seat_reservation_seat_id";
    public const string FKReservationReservationSeat = "FK_reservation_reservation_seat_id";
    public const string FKCinemaHallScreening = "FK_cinema_hall_screening_id";
    public const string FKCinemaHallSeat = "FK_cinema_hall_seat_id";
    public const string FKCinemaBuildingCinemaHall = "FK_cinema_building_cinema_hall_id";
    public const string FKMovieScreening = "FK_movie_screening_id";
    
    public const string UQSeatCinemaHallRowColumn = "IX_UQ_seat_cinema_hall_row_column";
    public const string UQCinemaHallCinemaBuildingIdNumber = "IX_UQ_cinema_hall_cinema_building_id_number";
}