using Motorcycles.Application.DTOs.Request.Motorcycles;
using Motorcycles.Application.DTOs.Response;

namespace Motorcycles.Application.Abstractions.Services;

public interface IMotorCycleService
{
    Task CreateMotorCycle(CreateMotorcycleRequestDto motorCycleRequest);
    Task<IEnumerable<MotorCyclesReponseDto>> GetMotorCycles(string? plate);
    Task<MotorCyclesReponseDto> GetMotorCycleByIdentifier(string identifier);
    Task UpdatePlateMotorcycle(string id, UpdatePlateMotorcycleRequestDto requestDto);
    Task DeleteMotorcycleByIdentifier(string identifier);
}
