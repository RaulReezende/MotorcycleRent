using System.Text.Json.Serialization;

namespace Motorcycles.Application.DTOs.Request.Motorcycles;

public class CreateMotorcycleRequestDto
{
    [JsonPropertyName("identificador")]
    public string Identifier { get; set; }

    [JsonPropertyName("ano")]
    public int Year { get; set; }

    [JsonPropertyName("modelo")]
    public string Model { get; set; }

    [JsonPropertyName("placa")]
    public string PlateNumber { get; set; }
}
