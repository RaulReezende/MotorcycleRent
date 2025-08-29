using Motorcycles.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motorcycles.Application.Abstractions.Services;

public interface IMotorcycleEventService
{
    Task CreateMotorcycleEvent(MotorcycleRegisteredEvent motorcycleRegisteredEvent);
}
