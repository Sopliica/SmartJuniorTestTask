using MediatR;
using SmartJuniorTestTask.Models;

namespace SmartJuniorTestTask.Features.EquipmentPlacementContracts.Commands;

public record CreateEquipmentPlacementContractCommand : IRequest<int>
{
    public int ProductionFacilityCode { get; set; }
    public int TypeOfProcessEquipmentCode { get; set; }
    public int NumberOfEquipmentUnits { get; set; }
}
