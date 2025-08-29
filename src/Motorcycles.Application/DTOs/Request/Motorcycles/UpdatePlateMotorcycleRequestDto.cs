using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Motorcycles.Application.DTOs.Request.Motorcycles;

public class UpdatePlateMotorcycleRequestDto
{
    [JsonPropertyName("placa")]
    public string Plate { get; set; }
}
