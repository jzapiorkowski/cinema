using Cinema.Domain.Shared.Interfaces;
using Cinema.Infrastructure.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Infrastructure.Shared.UnitOfWork;

internal class UnitOfWork : BaseUnitOfWork, IUnitOfWork
{
    private readonly DbContext _context;

    public UnitOfWork(ApplicationDbContext context, IServiceProvider serviceProvider) : base(context, serviceProvider)
    {
        _context = context;
    }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }
}