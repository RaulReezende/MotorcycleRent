using Microsoft.EntityFrameworkCore;
using Motorcycles.Domain.Abstractions.Repositories;
using Motorcycles.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motorcycles.Infraestructure.Repositories;

public class DeliveryManRepository(AppDbContext context) : IDeliveryManRepository
{
    private readonly AppDbContext _context = context;
    public async Task CreateAsync(DeliveryMan deliveryMan) =>
        await _context.DeliveryMens.AddAsync(deliveryMan);

    public Task UpdateAsync(DeliveryMan deliveryMan)
    {
        _context.DeliveryMens.Update(deliveryMan);
        return Task.CompletedTask;
    }

    public async Task<DeliveryMan?> GetByCnhAsync(string cnh) =>
        await _context.DeliveryMens.FirstOrDefaultAsync(x => x.CNH_Number == cnh);
    public async Task<DeliveryMan?> GetByIdentifierAsync(string identifier) =>
        await _context.DeliveryMens.FirstOrDefaultAsync(x => x.Identifier == identifier);
    public async  Task<DeliveryMan?> GetByCnpjAsync(string cnpj) =>
         await _context.DeliveryMens.FirstOrDefaultAsync(x => x.CNPJ == cnpj);


}
