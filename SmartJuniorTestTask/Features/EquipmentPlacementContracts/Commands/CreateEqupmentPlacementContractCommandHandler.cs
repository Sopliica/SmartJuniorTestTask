using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartJuniorTestTask.Models;
using SmartJuniorTestTask.Repos.Interfaces;

namespace SmartJuniorTestTask.Features.EquipmentPlacementContracts.Commands;

public class CreateEqupmentPlacementContractCommandHandler : IRequestHandler<CreateEquipmentPlacementContractCommand, int>
{
    private readonly IEquipmentPlacemntContractRepository _equipmentPlacemntContractRepository;
    private readonly IProductionFacilityRepository _facilityRepository;
    private readonly ITypeOfProcessEquipmentRepository _typeOfProcessEquipmentRepository;
    public CreateEqupmentPlacementContractCommandHandler(
        IEquipmentPlacemntContractRepository equipmentPlacemntContractRepository,
        IProductionFacilityRepository facilityRepository,
        ITypeOfProcessEquipmentRepository typeOfProcessEquipmentRepository)
    {
        _equipmentPlacemntContractRepository = equipmentPlacemntContractRepository;
        _facilityRepository = facilityRepository;
        _typeOfProcessEquipmentRepository = typeOfProcessEquipmentRepository;
    }
    public async Task<int> Handle(
        CreateEquipmentPlacementContractCommand request,
        CancellationToken cancellationToken)
    {
        var contractToAdd = new EquipmentPlacementContract
        {
            ProductionFacilityCode = request.ProductionFacilityCode,
            TypeOfProcessEquipmentCode = request.TypeOfProcessEquipmentCode,
            NumberOfEquipmentUnits = request.NumberOfEquipmentUnits
        };

        await ValidateIfThereIsEnoughFreeSpaceInFacility(contractToAdd);

        await _equipmentPlacemntContractRepository.Create(contractToAdd);
        return contractToAdd.Id;
    }

    private async Task ValidateIfThereIsEnoughFreeSpaceInFacility(EquipmentPlacementContract contractToAdd)
    {
        var currentContractEquipmentType= await _typeOfProcessEquipmentRepository
            .GetById(contractToAdd.TypeOfProcessEquipmentCode);
        var currentContractEquipmentTypeArea = currentContractEquipmentType.Area;
        
        var currentContractFacility = await _facilityRepository
            .GetById(contractToAdd.ProductionFacilityCode);
        var currentContractFacilityOverallSpace = currentContractFacility.StandardAreaForEquipment;

        var contractsWithCurrentFacility = await _equipmentPlacemntContractRepository
            .FindBy(x => x.ProductionFacilityCode == contractToAdd.ProductionFacilityCode);
        var contractsWithIncludedFacilitiesAndTypes = await contractsWithCurrentFacility
            .Include(c => c.TypeOfProcessEquipment)
            .Include(c => c.ProductionFacility)
            .ToListAsync();

        int currentFacilityOcuppiedSpace = 0;
        foreach (var contract in contractsWithIncludedFacilitiesAndTypes)
        {
            currentFacilityOcuppiedSpace += 
                contract.NumberOfEquipmentUnits * contract.TypeOfProcessEquipment.Area;
        }
        int requiredArea = contractToAdd.NumberOfEquipmentUnits * currentContractEquipmentTypeArea;
        if (currentContractFacilityOverallSpace - currentFacilityOcuppiedSpace < requiredArea)
            throw new ValidationException("There is not enough free space in this facility for equipment of this type in this quantity.");
    }
}
