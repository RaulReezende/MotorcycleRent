using Motorcycles.Application.DTOs.Response;
using Motorcycles.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motorcycles.Application.Extensions;

public static class MotorCycleExtensions
{
    public static MotorCyclesReponseDto ToDto(this Motorcycle motorcycle)
    {
        return new MotorCyclesReponseDto
        {
            Identificador = motorcycle.Identifier,
            Ano = motorcycle.Year,
            Modelo = motorcycle.Model,
            Placa = motorcycle.Plate
        };
    }

    public static IEnumerable<MotorCyclesReponseDto> ToDtos(this IEnumerable<Motorcycle> motorcycles)
    {
        return motorcycles.Select(m => m.ToDto());
    }
}
