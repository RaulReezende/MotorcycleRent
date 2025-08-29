using Motorcycles.Domain.Entities;

namespace Motorcycles.Infraestructure.Repositories;

public interface IMotorCycleRepository
{
    Task CreateAsync(Motorcycle motorcycle);
    Task UpdateAsync(Motorcycle motorcycle);
    Task<Motorcycle?> GetByPlateAsync(string plateNumber);
    Task<Motorcycle?> GetByIdentifierAsync(string identifier);
    Task<IEnumerable<Motorcycle>> GetAllMotorCyclesAsync();
    Task DeleteAsync(Motorcycle motorcycle);

}