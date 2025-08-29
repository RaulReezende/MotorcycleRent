using Motorcycles.Application.DTOs.Request.Rent;
using Motorcycles.Application.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motorcycles.Application.Abstractions.Services;

public interface IRentalService
{
    Task CreateRentMotorcycle(RentMotorcycleRequestDto requestDto);
    Task<RentMotorcycleResponseDto> GetRentMotorcycle(string id);
    Task UpdateDevolutionDate(UpdateDevolutionDateRequestDto requestDto, string id);
}
