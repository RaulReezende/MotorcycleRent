using Microsoft.AspNetCore.Http;
using Motorcycles.Application.DTOs.Request.DeliveryMan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motorcycles.Application.Abstractions.Services;

public interface IDeliveryManService
{
    Task CreateDeliveryMan(CreateDeliveryManRequestDto deliveryMan);
    Task UpdateCnhPhotoDeliveryMan(string id, CNHImageRequestDto imgCnh);

}
