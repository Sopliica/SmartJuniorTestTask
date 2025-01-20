using System.ComponentModel.DataAnnotations;

namespace SmartJuniorTestTask.Models;

public class ProductionFacility
{
    [Key]
    public int Code { get; set; }
    public string Name { get; set; }
    public int StandardAreaForEquipment { get; set; }
}
