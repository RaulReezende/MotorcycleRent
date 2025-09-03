using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Motorcycles.Application.Abstractions.Services;
using Motorcycles.Application.DTOs.Request.Motorcycles;

namespace MotorcycleRent.API.Controllers;

[ApiController]
[Route("motos")]
public class MotorCyclesController(IMotorCycleService motorCycleService) : Controller
{
    private readonly IMotorCycleService _motorCycleService = motorCycleService;
    
    [HttpPost]
    public async Task<IActionResult> CreateMotorCycle([FromBody] CreateMotorcycleRequestDto motorCycleRequest)
    {
        await _motorCycleService.CreateMotorCycle(motorCycleRequest);
        return Created();
    }

    [HttpGet()]
    public async Task<IActionResult> GetMotorCycles([FromQuery] string? placa)
    {
        return Ok(await _motorCycleService.GetMotorCycles(placa));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMotorCyclesByIdentifier(string id)
    {
        return Ok(await _motorCycleService.GetMotorCycleByIdentifier(id));
    }

    [HttpPut("{id}/placa")]
    public async Task<IActionResult> UpdatePlateMotorcycle(string id, UpdatePlateMotorcycleRequestDto requestDto)
    {
        await _motorCycleService.UpdatePlateMotorcycle(id, requestDto);
        return Ok(new { mensagem = "Placa modificada com sucesso" });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMotorCyclesByIdentifier(string id)
    {
        await _motorCycleService.DeleteMotorcycleByIdentifier(id);
        return Ok();
    }
}

