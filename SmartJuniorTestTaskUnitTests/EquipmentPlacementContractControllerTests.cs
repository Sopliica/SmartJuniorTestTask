using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using SmartJuniorTestTask.Controllers;
using SmartJuniorTestTask.Features.EquipmentPlacementContracts.Commands;
using SmartJuniorTestTask.Features.EquipmentPlacementContracts.DTOs;
using SmartJuniorTestTask.Features.EquipmentPlacementContracts.Queries;
using SmartJuniorTestTask.Models;
using SmartJuniorTestTask.Repos.Interfaces;

namespace SmartJuniorTestTaskUnitTests;

public class EquipmentPlacementContractControllerTests
{
    private readonly IMediator _mediator;
    private readonly EquipmentPlacementContractController _controller;

    public EquipmentPlacementContractControllerTests()
    {
        _mediator = Substitute.For<IMediator>();
        _controller = new EquipmentPlacementContractController(_mediator);
    }

    [Fact]
    public async Task CreateEquipmentPlacementContractWithCorrectRequestShouldReturnOkWithResult()
    {
        var command = new CreateEquipmentPlacementContractCommand
        {
            ProductionFacilityCode = 1,
            TypeOfProcessEquipmentCode = 2,
            NumberOfEquipmentUnits = 3
        };

        var expectedResult = 1;
        var mockRepository = Substitute.For<IEquipmentPlacemntContractRepository>();
        mockRepository.Create(Arg.Any<EquipmentPlacementContract>())
            .Returns(Task.FromResult(new EquipmentPlacementContract
            {
                Id = expectedResult,
                ProductionFacilityCode = command.ProductionFacilityCode,
                TypeOfProcessEquipmentCode = command.TypeOfProcessEquipmentCode,
                NumberOfEquipmentUnits = command.NumberOfEquipmentUnits
            }));

        var mediator = Substitute.For<IMediator>();
        mediator.Send(command).Returns(expectedResult);
        var controller = new EquipmentPlacementContractController(mediator);

        var result = await controller.CreateEquipmentPlacementContract(command);

        var okResult = result as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult.StatusCode.Should().Be(200);
        okResult.Value.Should().Be(expectedResult);
        await mediator.Received(1).Send(command);
    }

    [Fact]
    public async Task GetAllEquipmentPlacementContractsShouldReturnOkWithResult()
    {
        var expectedResult = new List<EquipmentPlacementContractDto>
        {
            new EquipmentPlacementContractDto
            {
                ProductionFacilityName = "Facility A",
                ProcessEquipmentTypeName = "Equipment Type X",
                EquipmentQuantityAccordingToTheContract = 10
            },
            new EquipmentPlacementContractDto
            {
                ProductionFacilityName = "Facility B",
                ProcessEquipmentTypeName = "Equipment Type Y",
                EquipmentQuantityAccordingToTheContract = 5
            }
        };
        _mediator.Send(Arg.Any<GetAllEquipmentPlacementContractsQuery>()).Returns(expectedResult);

        var result = await _controller.GetAllEquipmentPlacementContracts();

        var okResult = result as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult.StatusCode.Should().Be(200);
        okResult.Value.Should().BeEquivalentTo(expectedResult);
        await _mediator.Received(1).Send(Arg.Any<GetAllEquipmentPlacementContractsQuery>());
    }
}