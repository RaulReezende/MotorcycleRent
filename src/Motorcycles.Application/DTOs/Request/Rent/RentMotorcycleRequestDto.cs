using System.Text.Json.Serialization;

namespace Motorcycles.Application.DTOs.Request.Rent;

public class RentMotorcycleRequestDto
{
    [JsonPropertyName("entregador_id")]
    public string DeliveryManId { get; set; }

    [JsonPropertyName("moto_id")]
    public string MotorcycleId { get; set; }

    [JsonPropertyName("data_inicio")]
    public DateTime InitDate { get; set; }

    [JsonPropertyName("data_termino")]
    public DateTime EndDate { get; set; }

    [JsonPropertyName("data_previsao_termino")]
    public DateTime PrevEndDate { get; set; }

    [JsonPropertyName("plano")]
    public int Plan { get; set; }
}