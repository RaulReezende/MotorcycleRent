﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motorcycle.Consumer;

public static class DependencyInjection
{
    public static IServiceCollection AddConsumer(this IServiceCollection services)
    {
        services.AddHostedService<MotorcycleConsumerWorker>();

        return services;
    }
}
