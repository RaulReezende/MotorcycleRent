using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motorcycles.Infraestructure.RabbitMq;

public class RabbitMqConnection : IDisposable
{
    private readonly IConfiguration _configuration;
    private readonly IConnection _connection;
    private readonly IChannel _channel;
    private bool _disposed;

    public RabbitMqConnection(IConfiguration configuration)
    {
        _configuration = configuration;
        var connection = new ConnectionFactory()
        {
            HostName = _configuration["RabbitMq:Host"],
            Port = Int32.Parse(_configuration["RabbitMq:Port"]),
            Password = _configuration["RabbitMq:Password"]
        };
        _connection = connection.CreateConnectionAsync().Result;
        _channel = _connection.CreateChannelAsync().Result;

    }

    public IChannel Channel => _channel;

    public void Dispose()
    {
        if (_disposed) return;

        _channel?.CloseAsync();
        _connection?.CloseAsync();
        _disposed = true;
    }
}
