﻿using System.ComponentModel.DataAnnotations;

namespace SmartJuniorTestTask.Models;

public class EquipmentPlacementContract
{
    [Key]
    public int Id { get; set; }
    public ProductionFacility ProductionFacility { get; set; }
    public int ProductionFacilityCode { get; set; }
    public TypeOfProcessEquipment TypeOfProcessEquipment { get; set; }
    public int TypeOfProcessEquipmentCode { get; set; }
    public int NumberOfEquipmentUnits { get; set; }
}
