using Microsoft.AspNetCore.Mvc;
using Motorcycles.Application.Abstractions.Services;
using Motorcycles.Application.DTOs.Request.DeliveryMan;
using System.Buffers.Text;

namespace MotorcycleRent.API.Controllers;

[ApiController]
[Route("entregadores")]
public class DeliveryManController(IDeliveryManService deliveryManService) : Controller
{
    private readonly IDeliveryManService _deliveryManService = deliveryManService;

    [HttpPost]
    public async Task<IActionResult> CreateDeliveryMan([FromBody] CreateDeliveryManRequestDto deliveryManRequest)
    {
        await _deliveryManService.CreateDeliveryMan(deliveryManRequest);
        return Created();
    }

    [HttpPost("{id}/cnh")]
    public async Task<IActionResult> UpdateCnhPhotoDeliveryMan(string id, [FromBody] CNHImageRequestDto imagem_cnh)
    {
        await _deliveryManService.UpdateCnhPhotoDeliveryMan(id, imagem_cnh);
        return Created();
    }
}
