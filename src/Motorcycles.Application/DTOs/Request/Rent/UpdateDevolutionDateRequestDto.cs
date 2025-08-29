using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Motorcycles.Application.DTOs.Request.Rent;

public class UpdateDevolutionDateRequestDto
{
    [JsonPropertyName("data_devolucao")]
    public DateTime DevolutionDate { get; set; }
}
