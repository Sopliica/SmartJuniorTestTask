using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartJuniorTestTask.Features.EquipmentPlacementContracts.Commands;
using SmartJuniorTestTask.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using SmartJuniorTestTask.Features.EquipmentPlacementContracts;

namespace SmartJuniorTestTask.Controllers;

[ApiController]
[Route("controller")]
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
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _mediator.Send(command);

        return Ok(result);
    }
}
