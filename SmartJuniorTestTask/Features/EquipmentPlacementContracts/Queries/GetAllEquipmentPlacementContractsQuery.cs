using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartJuniorTestTask.Features.EquipmentPlacementContracts.DTOs;
using SmartJuniorTestTask.Models;
using SmartJuniorTestTask.Repos.Interfaces;

namespace SmartJuniorTestTask.Features.EquipmentPlacementContracts.Queries;

public record GetAllEquipmentPlacementContractsQuery : IRequest<IEnumerable<EquipmentPlacementContractDto>>;

public class GetAllEquipmentPlacementContractsHandler : 
    IRequestHandler<GetAllEquipmentPlacementContractsQuery, IEnumerable<EquipmentPlacementContractDto>>
{
    private readonly IEquipmentPlacemntContractRepository _equipmentPlacemntContractRepository;
    public GetAllEquipmentPlacementContractsHandler(
        IEquipmentPlacemntContractRepository equipmentPlacemntContractRepository)
    {
        _equipmentPlacemntContractRepository = equipmentPlacemntContractRepository;
    }
    public async Task<IEnumerable<EquipmentPlacementContractDto>> Handle(
        GetAllEquipmentPlacementContractsQuery request,
        CancellationToken cancellationToken)
    {
        var contracts = await _equipmentPlacemntContractRepository.GetAll();

        var projectedContracts = contracts
            .Include(c => c.ProductionFacility)
            .Include(c => c.TypeOfProcessEquipment)
            .Select(contracts => new EquipmentPlacementContractDto
            {
                ProductionFacilityName = contracts.ProductionFacility.Name,
                ProcessEquipmentTypeName = contracts.TypeOfProcessEquipment.Name,
                EquipmentQuantityAccordingToTheContract = contracts.NumberOfEquipmentUnits
            });

        return projectedContracts;
    }

}
