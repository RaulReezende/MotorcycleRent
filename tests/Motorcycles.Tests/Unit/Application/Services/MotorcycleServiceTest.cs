
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Motorcycles.Application.Abstractions.Services;
using Motorcycles.Application.DTOs.Request.DeliveryMan;
using Motorcycles.Application.DTOs.Request.Motorcycles;
using Motorcycles.Application.Services;
using Motorcycles.Domain.Abstractions.Repositories;
using Motorcycles.Domain.Abstractions.Storage;
using Motorcycles.Domain.Entities;
using Motorcycles.Domain.Exceptions;
using Motorcycles.Infraestructure.Repositories;
using Motorcycles.Tests.Unit.Application.Builders;

namespace Motorcycles.Tests.Unit.Application.Services;

public class MotorcycleServiceTest
{
    private readonly IMotorCycleService _motorcycleService;
    private readonly Mock<IMotorCycleRepository> _motorcycleRepository;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IValidator<CreateMotorcycleRequestDto>> _validator;
    private readonly Mock<IMessageService> _messageService;
    private readonly Mock<IRentalRepository> _rentalRepository;

    public MotorcycleServiceTest()
    {
        _motorcycleRepository = new Mock<IMotorCycleRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _validator = new Mock<IValidator<CreateMotorcycleRequestDto>>();
        _messageService = new Mock<IMessageService>();
        _rentalRepository = new Mock<IRentalRepository>();

        _motorcycleService = new MotorCycleService(_validator.Object, _motorcycleRepository.Object, _unitOfWorkMock.Object, _messageService.Object, _rentalRepository.Object);

    }

    [Fact]
    public async Task CreateMotorCycle_WhenIsRight_ShouldReturnSucess()
    {
        // Arrange
        var requestDto = CreateMotorcycleRequestDtoBuilder.Create().Build();
        var motorcycle = MotorcycleBuilder.Create().Build();
        var validationResult = new ValidationResult();
        _validator.Setup(v => v.ValidateAsync(requestDto, It.IsAny<CancellationToken>()))
            .ReturnsAsync(validationResult);

        _motorcycleRepository
            .Setup(repo => repo.GetByPlateAsync(requestDto.PlateNumber))
            .ReturnsAsync((Motorcycle?)null);

        _motorcycleRepository
            .Setup(repo => repo.CreateAsync(It.IsAny<Motorcycle>()))
            .Returns(Task.CompletedTask);

        _unitOfWorkMock
            .Setup(uow => uow.CommitAsync())
            .Returns(Task.CompletedTask);


        // Act
        await _motorcycleService.Invoking(s => s.CreateMotorCycle(requestDto))
            .Should().NotThrowAsync();

        _motorcycleRepository.Verify(
        repo => repo.CreateAsync(It.Is<Motorcycle>(m =>
            m.Identifier == requestDto.Identifier &&
            m.Plate == requestDto.PlateNumber &&
            m.Year == requestDto.Year &&
            m.Model == requestDto.Model)),
        Times.Once);

        _unitOfWorkMock.Verify(
        uow => uow.CommitAsync(),
        Times.Once);

        // Assert
    }

    [Fact]
    public async Task CreateMotorCycle_WhenMotorcycleExist_ShouldReturnError()
    {
        // Arrange
        var requestDto = CreateMotorcycleRequestDtoBuilder.Create().Build();
        var motorcycle = MotorcycleBuilder.Create().Build();
        var validationResult = new ValidationResult();
        _validator.Setup(v => v.ValidateAsync(requestDto, It.IsAny<CancellationToken>()))
            .ReturnsAsync(validationResult);

        _motorcycleRepository
            .Setup(repo => repo.GetByPlateAsync(requestDto.PlateNumber))
            .ReturnsAsync(motorcycle);

        // Act & Assert
        await _motorcycleService.Invoking(s => s.CreateMotorCycle(requestDto))
            .Should().ThrowAsync<NotFoundException>();

        _motorcycleRepository.Verify(repo => repo.CreateAsync(It.IsAny<Motorcycle>()), Times.Never);

        _unitOfWorkMock.Verify(
        uow => uow.CommitAsync(),
        Times.Never);
    }

    [Fact]
    public async Task CreateMotorCycle_WhenMotorcycleIs2024_ShouldReturnSuccess()
    {
        // Arrange
        var requestDto = CreateMotorcycleRequestDtoBuilder.Create().WithYear(2024).Build();
        var motorcycle = MotorcycleBuilder.Create().WithYear(2024).Build();
        var validationResult = new ValidationResult();
        _validator.Setup(v => v.ValidateAsync(requestDto, It.IsAny<CancellationToken>()))
            .ReturnsAsync(validationResult);

        _motorcycleRepository
            .Setup(repo => repo.GetByPlateAsync(requestDto.PlateNumber))
            .ReturnsAsync((Motorcycle?)null);

        _motorcycleRepository
            .Setup(repo => repo.CreateAsync(It.IsAny<Motorcycle>()))
            .Returns(Task.CompletedTask);

        _unitOfWorkMock
            .Setup(uow => uow.CommitAsync())
            .Returns(Task.CompletedTask);

        _messageService
            .Setup(serv => serv.PublishMotorcycleRegisteredEventAsync(It.IsAny<Motorcycle>()))
            .Returns(Task.CompletedTask);

        // Act
        await _motorcycleService.Invoking(s => s.CreateMotorCycle(requestDto))
            .Should().NotThrowAsync();

        _motorcycleRepository.Verify(
        repo => repo.CreateAsync(It.Is<Motorcycle>(m =>
            m.Identifier == requestDto.Identifier &&
            m.Plate == requestDto.PlateNumber &&
            m.Year == requestDto.Year &&
            m.Model == requestDto.Model)),
        Times.Once);

        _unitOfWorkMock.Verify(
        uow => uow.CommitAsync(),
        Times.Once);

        _messageService.Verify(
            repo => repo.PublishMotorcycleRegisteredEventAsync(It.Is<Motorcycle>(m =>
            m.Identifier == requestDto.Identifier &&
            m.Plate == requestDto.PlateNumber &&
            m.Year == requestDto.Year &&
            m.Model == requestDto.Model)),
            Times.Once);

    }
}
