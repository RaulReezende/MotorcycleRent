using Microsoft.Extensions.Logging;
using Motorcycles.Application.Abstractions.Services;
using Motorcycles.Domain.Abstractions.Repositories;
using Motorcycles.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motorcycles.Application.Services;

public class MotorcycleEventService(
    IMotorcycleEventRepository motorcycleEventRepository,
    IUnitOfWork unitOfWork,
    ILogger<MotorcycleEventService> logger) : IMotorcycleEventService
{
    private readonly IMotorcycleEventRepository _motorcycleEventRepository = motorcycleEventRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILogger<MotorcycleEventService> _logger = logger;
    public async Task CreateMotorcycleEvent(MotorcycleRegisteredEvent motorcycleRegisteredEvent)
    {
        await _motorcycleEventRepository.CreateAsync(motorcycleRegisteredEvent);
        await _unitOfWork.CommitAsync();

        _logger.LogInformation("Event sucessfully created");
    }
}
