using Motorcycles.Domain.Enums;
using System.Text.Json.Serialization;


namespace Motorcycles.Application.DTOs.Request.DeliveryMan;

public class CreateDeliveryManRequestDto
{
    [JsonPropertyName("identificador")]
    public string Identifier { get; set; }

    [JsonPropertyName("nome")]
    public string Name { get; set; }

    [JsonPropertyName("cnpj")]
    public string Cnpj { get; set; }

    [JsonPropertyName("data_nascimento")]
    public DateTime DateOfBirth { get; set; }

    [JsonPropertyName("numero_cnh")]
    public string CnhNumber { get; set; }

    [JsonPropertyName("tipo_cnh")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public CnhType CnhType { get; set; }

    [JsonPropertyName("imagem_cnh")]
    public string? CnhImageUrl { get; set; }
}
