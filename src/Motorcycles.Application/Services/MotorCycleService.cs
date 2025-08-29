using FluentValidation;
using Motorcycles.Application.Abstractions.Services;
using Motorcycles.Application.DTOs.Request.Motorcycles;
using Motorcycles.Application.DTOs.Response;
using Motorcycles.Application.Extensions;
using Motorcycles.Application.Validators.DeliveryMan;
using Motorcycles.Application.Validators.Motorcycle;
using Motorcycles.Domain.Abstractions.Repositories;
using Motorcycles.Domain.Entities;
using Motorcycles.Domain.Exceptions;
using Motorcycles.Infraestructure.Repositories;

namespace Motorcycles.Application.Services;

public sealed class MotorCycleService(
    IValidator<CreateMotorcycleRequestDto> validator,
    IMotorCycleRepository motorCycleRepository,
    IUnitOfWork unitOfWork,
    IMessageService messageService,
    IRentalRepository rentalRepository
    ) : IMotorCycleService
{
    private readonly IValidator<CreateMotorcycleRequestDto> _validator = validator;
    private readonly IMotorCycleRepository _motorCycleRepository = motorCycleRepository;
    private readonly IRentalRepository _rentalRepository = rentalRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMessageService _messageService = messageService;
    private const int YearMessaging = 2024;
    public async Task CreateMotorCyrcle(CreateMotorcycleRequestDto motorCycleRequest)
    {
        // validator
        await _validator.ValidateAndThrowAsync(motorCycleRequest);
        
        // execução
        if(await _motorCycleRepository.GetByPlateAsync(motorCycleRequest.PlateNumber) is not null)
            throw new NotFoundException();

        Motorcycle motorcycles = new()
        {
            Identifier = motorCycleRequest.Identifier,
            Year = motorCycleRequest.Year,
            Plate = motorCycleRequest.PlateNumber,
            Model = motorCycleRequest.Model,
        };

        await _motorCycleRepository.CreateAsync(motorcycles);

        await _unitOfWork.CommitAsync();

        if (motorCycleRequest.Year == YearMessaging)
            await _messageService.PublishMotorcycleRegisteredEventAsync(motorcycles);

    }

    public async Task<IEnumerable<MotorCyclesReponseDto>> GetMotorCycles(string? plate)
    {
        if(plate is null)
        {
            var motorcycles = await _motorCycleRepository.GetAllMotorCyclesAsync();
            return motorcycles.ToDtos();
        }

        var motorcycle = await _motorCycleRepository.GetByPlateAsync(plate);

        return motorcycle switch
        {
            null => [],
            Motorcycle collection => [collection.ToDto()],
        };
    }

    public async Task<MotorCyclesReponseDto> GetMotorCycleByIdentifier(string identifier)
    {
        var motorcycle = await _motorCycleRepository.GetByIdentifierAsync(identifier) ?? throw new NotFoundException("Moto não encontrada");
        return motorcycle.ToDto();
    }

    public async Task UpdatePlateMotorcycle(string identifier, UpdatePlateMotorcycleRequestDto requestDto)
    {
        // validação
        if (string.IsNullOrEmpty(requestDto.Plate))
            throw new ValidationException("");

        var motorcycle = await _motorCycleRepository.GetByIdentifierAsync(identifier) ?? throw new ValidationException("");

        motorcycle.Plate = requestDto.Plate;
        await _motorCycleRepository.UpdateAsync(motorcycle);
        await _unitOfWork.CommitAsync();
    }


    public async Task DeleteMotorcycleByIdentifier(string identifier)
    {
        var motorcycle = await _motorCycleRepository.GetByIdentifierAsync(identifier) ?? throw new ValidationException("");

        var rental = await _rentalRepository.GetByMotorcycle(motorcycle.Id);
        if (rental is not null)
            throw new ValidationException("");

        await _motorCycleRepository.DeleteAsync(motorcycle);
        await _unitOfWork.CommitAsync();
    }
}
