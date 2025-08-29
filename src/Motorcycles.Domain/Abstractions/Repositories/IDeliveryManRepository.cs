using Motorcycles.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motorcycles.Domain.Abstractions.Repositories;

public interface IDeliveryManRepository 
{
    Task<DeliveryMan?> GetByCnpjAsync(string cnpj);
    Task<DeliveryMan?> GetByCnhAsync(string cnh);
    Task CreateAsync(DeliveryMan deliveryMan);
    Task<DeliveryMan?> GetByIdentifierAsync(string identifier);
    Task UpdateAsync(DeliveryMan deliveryMan);
}
