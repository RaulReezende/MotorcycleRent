using Motorcycles.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motorcycles.Domain.Abstractions.Repositories;

public interface IMessageService
{
    Task PublishMotorcycleRegisteredEventAsync(Motorcycle motorcycle);
}
