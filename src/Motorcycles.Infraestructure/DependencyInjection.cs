using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Motorcycles.Domain.Abstractions.Messaging;
using Motorcycles.Domain.Abstractions.Repositories;
using Motorcycles.Domain.Abstractions.Storage;
using Motorcycles.Infraestructure.FileStorage;
using Motorcycles.Infraestructure.RabbitMq;
using Motorcycles.Infraestructure.Repositories;

namespace Motorcycles.Infraestructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfraestructure(this IServiceCollection service, IConfiguration configuration)
    {
        var connection = configuration.GetConnectionString("db");

        service.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(connection));

        service.AddSingleton<IRabbitMqConnection, RabbitMqConnection>();

        service.AddSingleton<IFileStorageService>(provider =>
           new FileStorageMinioService(
               configuration["Minio:Endpoint"],
               configuration["Minio:AccessKey"],
               configuration["Minio:SecretKey"]
           ));

        #region Repositories

        service.AddScoped<IDeliveryManRepository, DeliveryManRepository>();
        service.AddScoped<IMotorCycleRepository, MotorCycleRepository>();
        service.AddScoped<IRentalRepository, RentalRepository>();
        service.AddScoped<IMotorcycleEventRepository, MotorcycleEventRepository>();
        service.AddScoped<IUnitOfWork, UnitOfWork>();

        #endregion
        return service;
    }
}
