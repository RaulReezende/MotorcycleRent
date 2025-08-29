using Motorcycles.Domain.Abstractions.Repositories;
using Motorcycles.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motorcycles.Infraestructure.Repositories;

public class MotorcycleEventRepository(AppDbContext context) : IMotorcycleEventRepository
{
    private readonly AppDbContext _context = context;

    public async Task CreateAsync(MotorcycleRegisteredEvent deliveryMan) =>
       await _context.MotorcycleEvents.AddAsync(deliveryMan);

}
