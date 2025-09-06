
using Motorcycles.Application.DTOs.Request.Motorcycles;

namespace Motorcycles.Tests.Unit.Application.Builders;

public class CreateMotorcycleRequestDtoBuilder
{
    private readonly CreateMotorcycleRequestDto _dto = new()
    {
        Identifier = "moto1",
        Model = "Yamaha-1",
        PlateNumber = "112j310",
        Year = 2022
    };

    public static CreateMotorcycleRequestDtoBuilder Create() => new();

    public CreateMotorcycleRequestDto Build() => _dto;

    public CreateMotorcycleRequestDtoBuilder WithYear(int year)
    {
        _dto.Year = year;
        return this;
    }
}
