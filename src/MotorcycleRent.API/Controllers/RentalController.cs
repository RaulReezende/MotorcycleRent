using Microsoft.AspNetCore.Mvc;
using Motorcycles.Application.Abstractions.Services;
using Motorcycles.Application.DTOs.Request.DeliveryMan;
using Motorcycles.Application.DTOs.Request.Rent;
using Motorcycles.Application.Services;

namespace MotorcycleRent.API.Controllers;


[ApiController]
[Route("locacao")]
public class RentalController(IRentalService rentalService) : Controller
{
    private readonly IRentalService _rentalService = rentalService;
    
    [HttpPost]
    public async Task<IActionResult> RentMotorcycle([FromBody] RentMotorcycleRequestDto rentRequest)
    {
        await _rentalService.CreateRentMotorcycle(rentRequest);
        return Created(string.Empty, null);
    }

    [HttpGet("{id}/")]
    public async Task<IActionResult> GetRentalMotorcycle(string id)
    {
        var rentalMotorcycle = await _rentalService.GetRentMotorcycle(id);
        return Ok(rentalMotorcycle);
    }

    [HttpPut("{id}/devolucao")]
    public async Task<IActionResult> UpdateDevolutionRentalMotorcycle(string id, [FromBody] UpdateDevolutionDateRequestDto rentRequest)
    {
        await _rentalService.UpdateDevolutionDate(rentRequest, id);
        return Ok(new { mensagem = "Data de devolução informada com sucesso" });
    }
}
