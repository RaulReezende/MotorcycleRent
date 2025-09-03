
using FluentAssertions;
using FluentValidation;
using Moq;
using Motorcycles.Application.Abstractions.Services;
using Motorcycles.Application.DTOs.Request.DeliveryMan;
using Motorcycles.Application.DTOs.Request.Motorcycles;
using Motorcycles.Application.Services;
using Motorcycles.Domain.Abstractions.Repositories;
using Motorcycles.Domain.Abstractions.Storage;
using Motorcycles.Domain.Entities;
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
        var requestDto = CreateMotorcycleRequestDtoBuilder.Create().WithId("moto1").Build();
        var motorcycle = MotorcycleBuilder.Create().Build();
        _validator
            .Setup(vali => vali.ValidateAndThrowAsync(requestDto, default))
            .Returns(Task.CompletedTask);

        _motorcycleRepository
            .Setup(repo => repo.GetByPlateAsync(requestDto.PlateNumber))
            .ReturnsAsync((Motorcycle?)null);

        _motorcycleRepository
            .Setup(repo => repo.CreateAsync(motorcycle))
            .Returns(Task.CompletedTask);

        _unitOfWorkMock
            .Setup(uow => uow.CommitAsync())
            .Returns(Task.CompletedTask);


        // Act
        await _motorcycleService.Invoking(s => s.CreateMotorCycle(requestDto))
            .Should().NotThrowAsync();

        _unitOfWorkMock.Verify(
        uow => uow.CommitAsync(),
        Times.Once);

        _motorcycleRepository.Verify(
            repo => repo.CreateAsync(motorcycle),
            Times.Once);
        // Assert
    }
}
