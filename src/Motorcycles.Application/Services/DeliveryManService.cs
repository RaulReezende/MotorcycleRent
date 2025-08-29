using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Motorcycles.Application.Abstractions.Services;
using Motorcycles.Application.DTOs.Request.DeliveryMan;
using Motorcycles.Domain.Abstractions.Repositories;
using Motorcycles.Domain.Abstractions.Storage;
using Motorcycles.Domain.Entities;
using Motorcycles.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motorcycles.Application.Services;

public sealed class DeliveryManService(
        IDeliveryManRepository deliveryManRepository,
        IUnitOfWork unitOfWork,
        IValidator<CreateDeliveryManRequestDto> validator,
        IFileStorageService fileStorageSerivce
    ) : IDeliveryManService
{
    private readonly IDeliveryManRepository _deliveryManRepository = deliveryManRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IValidator<CreateDeliveryManRequestDto> _validator = validator;
    private readonly IFileStorageService _fileStorageService = fileStorageSerivce;

    public async Task CreateDeliveryMan(CreateDeliveryManRequestDto deliveryMan)
    {
        // validação
        _validator.ValidateAndThrow(deliveryMan);
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

        await _deliveryManRepository.CreateAsync(deliveryManInsert);
        await _unitOfWork.CommitAsync();

    }

    public async Task UpdateCnhPhotoDeliveryMan(string id, CNHImageRequestDto imgCnh)
    {
        var deliveryMan = await _deliveryManRepository.GetByIdentifierAsync(id) ?? throw new NotFoundException("Dados inválidos");

        // Validar tamanho (máximo 5MB para CNH)
        //if (formFile.Length > 5 * 1024 * 1024)
        //    throw new NotFoundException("Imagem muito grande. Tamanho máximo: 5MB");

        // Processar imagem
        var fileName = $"cnh_{DateTime.Now:yyyyMMddHHmmss}_{Guid.NewGuid().ToString().Substring(0, 8)}.png";
        if (imgCnh.ImagemCnh.Contains(","))
            imgCnh.ImagemCnh = imgCnh.ImagemCnh.Split(',')[1];

        // Converter base64 para bytes
        var bytes = Convert.FromBase64String(imgCnh.ImagemCnh);

        using var stream = new MemoryStream(bytes);

        
        var filename = await _fileStorageService.UploadPhotoCnh(stream, fileName, bytes.Length, "image/png");

        deliveryMan.CNH_Image = filename;
        await _deliveryManRepository.UpdateAsync(deliveryMan);
    }

    private async Task<bool> DeliveryManExists(CreateDeliveryManRequestDto deliveryMan)
    {
        DeliveryMan? deliveryManEnt = await _deliveryManRepository.GetByCnpjAsync(deliveryMan.Cnpj) ?? await _deliveryManRepository.GetByCnhAsync(deliveryMan.CnhNumber);
        return deliveryManEnt is not null;
    }

}
