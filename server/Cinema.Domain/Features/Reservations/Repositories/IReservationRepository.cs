using Cinema.Domain.Features.Reservations.Entities;
using Cinema.Domain.Shared.Interfaces;

namespace Cinema.Domain.Features.Reservations.Repositories;

public interface IReservationRepository : IBaseRepository<Reservation>
{
}