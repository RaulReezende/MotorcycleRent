using Motorcycles.Application.DTOs.Response;
using Motorcycles.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motorcycles.Application.Extensions;

public static class RentalExtensions
{
    public static RentMotorcycleResponseDto ToDto(this Rental rental)
    {
        return new RentMotorcycleResponseDto()
        {
            Identificador = rental.Id.ToString(),
            Valor_diaria = rental.DailyValue,
            Entregador_id = rental.DeliveryMan.Identifier,
            Moto_id = rental.Motorcycle.Identifier,
            Data_inicio = rental.InitDate,
            Data_termino = rental.EndDate,
            Data_previsao_termino = rental.PrevEndDate,
            Data_devolucao = rental.DevolutionDate
        };
    }
}
