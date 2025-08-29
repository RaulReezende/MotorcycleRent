using Motorcycles.Domain.Abstractions.Repositories;

namespace Motorcycles.Infraestructure.Repositories;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    private readonly AppDbContext _context = context;
    public async Task CommitAsync() =>
           await _context.SaveChangesAsync();
}
