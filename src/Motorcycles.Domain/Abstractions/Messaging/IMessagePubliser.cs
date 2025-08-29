using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motorcycles.Domain.Abstractions.Messaging;

public interface IMessagePublisher
{
    Task PublishAsync<T>(T message, string topic) where T : class;
}
