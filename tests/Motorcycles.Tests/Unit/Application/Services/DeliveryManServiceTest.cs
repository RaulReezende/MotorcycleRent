using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation;
using Moq;
using Motorcycles.Application.DTOs.Request.DeliveryMan;
using Motorcycles.Application.Services;
using Motorcycles.Domain.Abstractions.Repositories;
using Motorcycles.Domain.Abstractions.Storage;
using Motorcycles.Domain.Entities;
using Motorcycles.Domain.Exceptions;
using Motorcycles.Tests.Unit.Application.Builders;

namespace Motorcycles.Tests.Unit.Application.Services;

public class DeliveryManServiceTest
{
    private readonly DeliveryManService _deliveryManService;
    private readonly Mock<IDeliveryManRepository> _deliveryManRepositoryMock ;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IValidator<CreateDeliveryManRequestDto>> _validator ;
    private readonly Mock<IFileStorageService> _fileStorageService;

    public DeliveryManServiceTest()
    {
        _deliveryManRepositoryMock = new Mock<IDeliveryManRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _validator = new Mock<IValidator<CreateDeliveryManRequestDto>>();
        _fileStorageService = new Mock<IFileStorageService>();
        _deliveryManService =
            new DeliveryManService(_deliveryManRepositoryMock.Object, _unitOfWorkMock.Object, _validator.Object, _fileStorageService.Object);
        SetupForSucess();
    }

    [Fact]
    public async Task CreateDelivery_WhenDeliveryManNotExist_ShouldReturnDeliveryMan()
    {
        // Arrange
        CreateDeliveryManRequestDto expectedDto = CreateDeliveryManDtoBuilder.Create().Build();
        DeliveryMan expectedDeliveryMan = DeliveryManBuilder.Create().Build();

        // Act & Assert
        await _deliveryManService.Invoking(s => s.CreateDeliveryMan(expectedDto))
            .Should().NotThrowAsync();

        _deliveryManRepositoryMock.Verify(
        repo => repo.CreateAsync(It.Is<DeliveryMan>(dm =>
            dm.Identifier == expectedDto.Identifier &&
            dm.Name == expectedDto.Name &&
            dm.CNPJ == expectedDto.Cnpj &&
            dm.Birth_Date == expectedDto.DateOfBirth &&
            dm.CNH_Number == expectedDto.CnhNumber &&
            dm.CNH_Type == expectedDto.CnhType.ToString()
        )),
        Times.Once
    );

        _unitOfWorkMock.Verify(
        uow => uow.CommitAsync(),
        Times.Once);

    }

    [Fact]
    public async Task CreateDeliveryMan_WhenDeliveryManCnpjExists_ShouldReturnError()
    {
        // Arrange
        var expectedDto = CreateDeliveryManDtoBuilder.Create().Build();
        var expectedDeliveryMan = DeliveryManBuilder.Create().Build();
       
        _deliveryManRepositoryMock
            .Setup(repo => repo.GetByCnpjAsync(expectedDto.Cnpj))
            .ReturnsAsync(expectedDeliveryMan);

        // Act
        var exception = await Record.ExceptionAsync(() =>
            _deliveryManService.CreateDeliveryMan(expectedDto));

        // Assert
        Assert.NotNull(exception);
        Assert.IsType<NotFoundException>(exception);
    }

    [Fact]
    public async Task CreateDeliveryMan_WhenDeliveryManCnhExists_ShouldReturnError()
    {
        // Arrange
        var expectedDto = CreateDeliveryManDtoBuilder.Create().Build();
        var expectedDeliveryMan = DeliveryManBuilder.Create().Build();
        
        _deliveryManRepositoryMock
            .Setup(repo => repo.GetByCnhAsync(expectedDto.CnhNumber))
            .ReturnsAsync(expectedDeliveryMan);

        // Act & Assert
        await _deliveryManService.Invoking(s => s.CreateDeliveryMan(expectedDto))
            .Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task UpdateCnhPhotoDeliveryMan_WhenDeliveryManExists_ShouldReturnSucess()
    {
        //Arrange
        var cnhImageBase64 = new CNHImageRequestDto { ImagemCnh = "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAYAAAAfFcSJAAAADUlEQVR42mP8z8BQDwAEhQGAhKmMIQAAAABJRU5ErkJggg==" };
        var deliveryMan = DeliveryManBuilder.Create().Build();
        _deliveryManRepositoryMock
            .Setup(repo => repo.GetByIdentifierAsync(deliveryMan.Identifier))
            .ReturnsAsync(deliveryMan);

        _fileStorageService
            .Setup(serv => serv.UploadPhotoCnh(cnhImageBase64.ImagemCnh))
            .ReturnsAsync(cnhImageBase64.ImagemCnh);

        _deliveryManRepositoryMock
            .Setup(repo => repo.UpdateAsync(deliveryMan))
            .Returns(Task.CompletedTask);

        // Act & Assert
        await _deliveryManService.Invoking(s => s.UpdateCnhPhotoDeliveryMan(deliveryMan.Identifier, cnhImageBase64))
            .Should().NotThrowAsync();
    }

    [Fact]
    public async Task UpdateCnhPhotoDeliveryMan_WhenNotDeliveryManExists_ShouldReturnError()
    {
        //Arrange
        var cnhImageBase64 = new CNHImageRequestDto { ImagemCnh = "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAYAAAAfFcSJAAAADUlEQVR42mP8z8BQDwAEhQGAhKmMIQAAAABJRU5ErkJggg==" };
        var deliveryMan = DeliveryManBuilder.Create().Build();
        _deliveryManRepositoryMock
            .Setup(repo => repo.GetByIdentifierAsync(It.IsAny<string>()))
            .ReturnsAsync((DeliveryMan?)null);

        // Act & Assert
        await _deliveryManService.Invoking(s => s.UpdateCnhPhotoDeliveryMan(deliveryMan.Identifier, cnhImageBase64))
            .Should().ThrowAsync<NotFoundException>();
    }

    private void SetupForSucess()
    {
        _deliveryManRepositoryMock
        .Setup(repo => repo.GetByCnpjAsync(It.IsAny<string>()))
        .ReturnsAsync((DeliveryMan?)null);

        _deliveryManRepositoryMock
            .Setup(repo => repo.GetByCnhAsync(It.IsAny<string>()))
            .ReturnsAsync((DeliveryMan?)null);

        _deliveryManRepositoryMock
            .Setup(repo => repo.CreateAsync(It.IsAny<DeliveryMan>()))
            .Returns(Task.CompletedTask);

        _unitOfWorkMock
        .Setup(uow => uow.CommitAsync())
        .Returns(Task.CompletedTask);
    }
}
