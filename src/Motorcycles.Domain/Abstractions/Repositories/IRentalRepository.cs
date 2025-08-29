using Motorcycles.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motorcycles.Domain.Abstractions.Repositories;

public interface IRentalRepository
{
    Task<Rental?> GetByIdentifierAsync(int identifier);
    Task CreateRentalAsync(Rental rental);
    Task UpdateRentalAsync(Rental rental);
    Task<Rental?> GetByMotorcycle(int motorcycleId);
}
