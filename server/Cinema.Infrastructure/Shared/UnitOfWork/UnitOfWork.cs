using Cinema.Domain.Shared.Interfaces;
using Cinema.Infrastructure.Core.Data;

namespace Cinema.Infrastructure.Shared.UnitOfWork;

internal class UnitOfWork : BaseUnitOfWork, IUnitOfWork
{
    public UnitOfWork(ApplicationDbContext context, IServiceProvider serviceProvider) : base(context, serviceProvider)
    {
    }
}