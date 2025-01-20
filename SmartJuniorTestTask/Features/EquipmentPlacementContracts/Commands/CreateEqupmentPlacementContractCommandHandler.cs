using MediatR;
using SmartJuniorTestTask.Models;
using SmartJuniorTestTask.Repos.Interfaces;

namespace SmartJuniorTestTask.Features.EquipmentPlacementContracts.Commands;

public class CreateEqupmentPlacementContractCommandHandler : IRequestHandler<CreateEquipmentPlacementContractCommand, int>
{
    private readonly IEquipmentPlacemntContractRepository _equipmentPlacemntContractRepository;
    public CreateEqupmentPlacementContractCommandHandler(
        IEquipmentPlacemntContractRepository equipmentPlacemntContractRepository)
    {
        _equipmentPlacemntContractRepository = equipmentPlacemntContractRepository;
    }
    public async Task<int> Handle(CreateEquipmentPlacementContractCommand request, CancellationToken cancellationToken)
    {
        var contractToAdd = new EquipmentPlacementContract
        {
            ProductionFacilityCode = request.ProductionFacilityCode,
            TypeOfProcessEquipmentCode = request.TypeOfProcessEquipmentCode,
            NumberOfEquipmentUnits = request.NumberOfEquipmentUnits
        };

        await _equipmentPlacemntContractRepository.Create(contractToAdd);
        return contractToAdd.Id;
    }
}
