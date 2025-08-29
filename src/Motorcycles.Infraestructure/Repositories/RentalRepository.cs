using Microsoft.EntityFrameworkCore;
using Motorcycles.Domain.Abstractions.Repositories;
using Motorcycles.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motorcycles.Infraestructure.Repositories;

public class RentalRepository(AppDbContext context) : IRentalRepository
{
    private readonly AppDbContext _context = context;

    public async Task<Rental?> GetByIdentifierAsync(int identifier) =>
       await _context.Rentals.Include(x => x.DeliveryMan).Include(x => x.Motorcycle).FirstOrDefaultAsync(x => x.Id == identifier);

    public async Task CreateRentalAsync(Rental rental) =>
        await _context.Rentals.AddAsync(rental);

    public Task UpdateRentalAsync(Rental rental) 
    {
        _context.Rentals.Update(rental);
        return Task.CompletedTask;
    }

    public async Task<Rental?> GetByMotorcycle(int motorcycleId) =>
        await _context.Rentals.FirstOrDefaultAsync(x => x.MotorcycleId == motorcycleId);

}
