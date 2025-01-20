using System.ComponentModel.DataAnnotations;

namespace SmartJuniorTestTask.Models;

public class TypeOfProcessEquipment
{
    [Key]
    public int Code { get; set; }
    public string Name { get; set; }
    public int Area { get; set; }
}
