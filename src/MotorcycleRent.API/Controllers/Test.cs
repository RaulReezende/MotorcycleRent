using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Motorcycles.Infraestructure;

namespace MotorcycleRent.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class Test : ControllerBase
{
    private readonly AppDbContext _context;

    public Test(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var deliveryMen = await _context.Motorcycles.ToListAsync();
        return Ok(deliveryMen);
    }
}
