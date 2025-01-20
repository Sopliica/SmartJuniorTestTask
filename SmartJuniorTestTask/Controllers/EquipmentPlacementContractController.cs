using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartJuniorTestTask.Features.EquipmentPlacementContracts.Commands;
using SmartJuniorTestTask.Features.EquipmentPlacementContracts.Queries;

namespace SmartJuniorTestTask.Controllers;

[ApiController]
[Route("equipmentPlacementContract")]
public class EquipmentPlacementContractController : Controller
{
    private readonly IMediator _mediator;
    public EquipmentPlacementContractController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateEquipmentPlacementContract(
        [FromBody] CreateEquipmentPlacementContractCommand command)
    {
        var result = await _mediator.Send(command);

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllEquipmentPlacementContracts()
    {
        var result = await _mediator.Send(new GetAllEquipmentPlacementContractsQuery());
        return Ok(result);
    }
}
