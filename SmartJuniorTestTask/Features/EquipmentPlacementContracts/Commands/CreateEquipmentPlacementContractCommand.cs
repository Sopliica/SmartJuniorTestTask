using MediatR;
using SmartJuniorTestTask.Models;

namespace SmartJuniorTestTask.Features.EquipmentPlacementContracts.Commands;

public record CreateEquipmentPlacementContractCommand : IRequest<int>
{
    public ProductionFacility ProductionFacility { get; set; }
    public TypeOfProcessEquipment TypeOfProcessEquipment { get; set; }
    public int NumberOfEquipmentUnits { get; set; }
}
