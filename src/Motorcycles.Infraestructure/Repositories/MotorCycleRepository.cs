
using Microsoft.EntityFrameworkCore;
using Motorcycles.Domain.Entities;

namespace Motorcycles.Infraestructure.Repositories;

public class MotorCycleRepository(AppDbContext context) : IMotorCycleRepository
{
    private readonly AppDbContext _context = context;

    public async Task CreateAsync(Motorcycle motorcycle) =>
        await _context.Motorcycles.AddAsync(motorcycle);

    public Task DeleteAsync(Motorcycle motorcycle)
    {
        _context.Motorcycles.Remove(motorcycle);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Motorcycle motorcycle)
    {
        _context.Motorcycles.Update(motorcycle);
        return Task.CompletedTask;
    }

    public async Task<Motorcycle?> GetByPlateAsync(string plateNumber) =>
        await _context.Motorcycles.FirstOrDefaultAsync(x => x.Plate == plateNumber);

    public async Task<Motorcycle?> GetByIdentifierAsync(string identifier) =>
        await _context.Motorcycles.FirstOrDefaultAsync(x => x.Identifier == identifier);

    public async Task<IEnumerable<Motorcycle>> GetAllMotorCyclesAsync() =>
        await _context.Motorcycles.ToListAsync();
}
