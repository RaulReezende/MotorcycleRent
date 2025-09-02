using FluentValidation;
using Motorcycles.Application.Abstractions.Services;
using Motorcycles.Application.DTOs.Request.DeliveryMan;
using Motorcycles.Domain.Abstractions.Repositories;
using Motorcycles.Domain.Abstractions.Storage;
using Motorcycles.Domain.Entities;
using Motorcycles.Domain.Exceptions;

namespace Motorcycles.Application.Services;

public sealed class DeliveryManService(
        IDeliveryManRepository deliveryManRepository,
        IUnitOfWork unitOfWork,
        IValidator<CreateDeliveryManRequestDto> validator,
        IFileStorageService fileStorageService
    ) : IDeliveryManService
{
    public async Task CreateDeliveryMan(CreateDeliveryManRequestDto deliveryMan)
    {
        // validação
        validator.ValidateAndThrow(deliveryMan);
        // ver se o entregador existe
        if ( await DeliveryManExists(deliveryMan))
            throw new NotFoundException();
        // inserir o entregador

        DeliveryMan deliveryManInsert = new()
        {
            Identifier = deliveryMan.Identifier,
            Name = deliveryMan.Name,
            CNPJ = deliveryMan.Cnpj,
            Birth_Date = deliveryMan.DateOfBirth,
            CNH_Number = deliveryMan.CnhNumber,
            CNH_Type = deliveryMan.CnhType.ToString(),
        };

        await deliveryManRepository.CreateAsync(deliveryManInsert);
        await unitOfWork.CommitAsync();

    }

    public async Task UpdateCnhPhotoDeliveryMan(string id, CNHImageRequestDto imgCnh)
    {
        var deliveryMan = await deliveryManRepository.GetByIdentifierAsync(id) ?? throw new NotFoundException("Dados inválidos");

        var filename = await fileStorageService.UploadPhotoCnh(imgCnh.ImagemCnh);

        deliveryMan.CNH_Image = filename;
        await deliveryManRepository.UpdateAsync(deliveryMan);
    }

    private async Task<bool> DeliveryManExists(CreateDeliveryManRequestDto deliveryMan)
    {
        DeliveryMan? deliveryManEnt = await deliveryManRepository.GetByCnpjAsync(deliveryMan.Cnpj) ?? await deliveryManRepository.GetByCnhAsync(deliveryMan.CnhNumber);
        return deliveryManEnt is not null;
    }

}
