using RabbitMQ.Client;

namespace Motorcycles.Domain.Abstractions.Messaging;

public interface IRabbitMqConnection
{
    IChannel Channel { get; }
}
