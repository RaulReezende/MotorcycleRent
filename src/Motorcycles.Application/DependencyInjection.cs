using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Motorcycles.Application.Abstractions.Services;
using Motorcycles.Application.DTOs.Request.DeliveryMan;
using Motorcycles.Application.DTOs.Request.Motorcycles;
using Motorcycles.Application.Messaging;
using Motorcycles.Application.Services;
using Motorcycles.Application.Validators.DeliveryMan;
using Motorcycles.Application.Validators.Motorcycle;
using Motorcycles.Domain.Abstractions.Messaging;
using Motorcycles.Domain.Abstractions.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Motorcycles.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection service)
    {
        #region Services

        service.AddScoped<IDeliveryManService, DeliveryManService>();
        service.AddScoped<IMotorCycleService, MotorCycleService>();
        service.AddScoped<IRentalService, RentalService>();
        service.AddScoped<IMessageService, MessagingService>();
        service.AddScoped<IMotorcycleEventService, MotorcycleEventService>();
        service.AddScoped<IMessagePublisher, Publisher>();

        #endregion

        #region Validator

        service.AddScoped<IValidator<CreateDeliveryManRequestDto>, CreateDeliveryManValidator>();
        service.AddScoped<IValidator<CreateMotorcycleRequestDto>, CreateMotorcycleValidator>();

        #endregion
        return service;
    }
}
