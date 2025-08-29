using System.Text.Json.Serialization;


namespace Motorcycles.Application.DTOs.Request.DeliveryMan;

public class CNHImageRequestDto
{
    [JsonPropertyName("imagem_cnh")]
    public string ImagemCnh { get; set; }
}
