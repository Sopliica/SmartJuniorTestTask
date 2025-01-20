using FluentValidation;
using SmartJuniorTestTask.Features.EquipmentPlacementContracts.Commands;

namespace SmartJuniorTestTask.Features.EquipmentPlacementContracts.Validators;

public class CreateEquipmentPlacementContractCommandValidator :
    AbstractValidator<CreateEquipmentPlacementContractCommand>
{
    public CreateEquipmentPlacementContractCommandValidator()
    {
        RuleFor(x => x.TypeOfProcessEquipmentCode)
            .NotEmpty()
            .WithMessage("Type of Precess Equipment Code can't be empty.");

        RuleFor(x => x.ProductionFacilityCode)
            .NotEmpty()
            .WithMessage("Production Facility Code can't be empty.");

        RuleFor(x => x.NumberOfEquipmentUnits)
            .NotEmpty()
            .WithMessage("Number of Equipment Units Code can't be empty.")
            .GreaterThan(0)
            .WithMessage("Number of Equipment Units have to be greater then 0.");
    }
}
