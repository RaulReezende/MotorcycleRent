using Motorcycles.Application.Abstractions.Services;
using Motorcycles.Application.DTOs.Request.Rent;
using Motorcycles.Application.DTOs.Response;
using Motorcycles.Application.Extensions;
using Motorcycles.Domain.Abstractions.Repositories;
using Motorcycles.Domain.Entities;
using Motorcycles.Domain.Exceptions;
using Motorcycles.Domain.ValueObjects;
using Motorcycles.Infraestructure.Repositories;
using System.ComponentModel.DataAnnotations;

namespace Motorcycles.Application.Services;

public class RentalService(
        IRentalRepository rentalRepository,
        IMotorCycleRepository motorCycleRepository,
        IDeliveryManRepository deliveryManRepository,
        IUnitOfWork unitOfWork
    ) : IRentalService
{
    private static readonly List<LocationPlan> Planos = new()
    {
        new LocationPlan ( Days: 7, DailyValue: 30.00m, PercentualFee: 0.20m ),
        new LocationPlan ( Days: 15, DailyValue: 28.00m, PercentualFee: 0.40m ),
        new LocationPlan ( Days: 30, DailyValue: 22.00m, PercentualFee: 0.0m ),
        new LocationPlan ( Days: 45, DailyValue: 20.00m, PercentualFee: 0.0m ),
        new LocationPlan ( Days: 50, DailyValue: 18.00m, PercentualFee: 0.0m )
    };

    private const decimal DailyValueAdditional = 50.00m;

    private readonly IRentalRepository _rentalRepository = rentalRepository;
    private readonly IDeliveryManRepository _deliveryManRepository = deliveryManRepository;
    private readonly IMotorCycleRepository _motorCycleRepository = motorCycleRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task CreateRentMotorcycle(RentMotorcycleRequestDto requestDto)
    {
        var deliveryMan = await _deliveryManRepository.GetByIdentifierAsync(requestDto.DeliveryManId) ?? throw new ValidationException();
        var motorCycle = await _motorCycleRepository.GetByIdentifierAsync(requestDto.MotorcycleId) ?? throw new ValidationException();

        if (deliveryMan.CNH_TypeEnum == Domain.Enums.CnhType.B) throw new ValidationException();

        var plano = Planos.FirstOrDefault(p => p.Days == requestDto.Plan) ?? throw new ValidationException();

        if (requestDto.PrevEndDate <= DateTime.Today) throw new ValidationException();

        var InitDate = DateTime.Today.AddDays(1);
        var PrevEndDate = InitDate.AddDays(plano.Days);

        Rental rental = new()
        {
            DeliveryManId = deliveryMan.Id,
            MotorcycleId = motorCycle.Id,
            DailyValue = plano.DailyValue,
            InitDate = InitDate,
            PrevEndDate = PrevEndDate,
            EndDate = requestDto.EndDate,
            Plan = requestDto.Plan,
            DeliveryMan = deliveryMan,
            Motorcycle = motorCycle
        };

        await _rentalRepository.CreateRentalAsync(rental);
        await _unitOfWork.CommitAsync();
    }

    public async Task<RentMotorcycleResponseDto> GetRentMotorcycle(string id)
    {
        if (!int.TryParse(id, out int idInt))
            throw new NotFoundException("Dados inválidos");

        var rental = await _rentalRepository.GetByIdentifierAsync(idInt) ?? throw new NotFoundException("Locação não encontrada");

        return rental.ToDto();
    }

    public async Task UpdateDevolutionDate(UpdateDevolutionDateRequestDto requestDto, string id)
    {
        if (!int.TryParse(id, out int idInt))
            throw new ValidationException();

        if (requestDto.DevolutionDate <= DateTime.Today)
            throw new ValidationException();

        var rental = await _rentalRepository.GetByIdentifierAsync(idInt) ?? throw new NotFoundException("Locação não encontrada");

        var plan = Planos.FirstOrDefault(p => p.Days == rental.Plan) ?? throw new ValidationException();

        var daysPlan = plan.Days;
        var dailyValue = daysPlan * plan.DailyValue;

        decimal valueFine = 0;
        decimal additionalValue = 0;
        int usedDays = 0;

        usedDays = (requestDto.DevolutionDate - rental.InitDate).Days;
        usedDays = Math.Max(0, usedDays);

        if (requestDto.DevolutionDate < rental.PrevEndDate)
        {
            var notUtilizedDays = (requestDto.DevolutionDate - rental.PrevEndDate).Days;
            var notUtilizedDailyValues = notUtilizedDays * plan.DailyValue;

            valueFine = notUtilizedDailyValues * plan.PercentualFee;

            dailyValue = usedDays * plan.DailyValue;
        }
        else if (requestDto.DevolutionDate > rental.PrevEndDate)
        {
            int aditionalDays = (rental.PrevEndDate - requestDto.DevolutionDate).Days;
            additionalValue = aditionalDays * DailyValueAdditional;
        }

        var valorTotal = dailyValue + valueFine + additionalValue;

        rental.TotalValue = valorTotal;
        rental.DevolutionDate = requestDto.DevolutionDate;
        rental.EndDate = requestDto.DevolutionDate;

        await _rentalRepository.UpdateRentalAsync(rental);
        await _unitOfWork.CommitAsync();
    }
}

