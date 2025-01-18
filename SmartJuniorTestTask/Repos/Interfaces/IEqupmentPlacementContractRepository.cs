using SmartJuniorTestTask.Models;
using System.Linq.Expressions;

namespace SmartJuniorTestTask.Repos.Interfaces;

public interface IEquipmentPlacemntContractRepository
{
    Task<EquipmentPlacementContract> Create(EquipmentPlacementContract equipmentPlacemntContract);
    Task<EquipmentPlacementContract> Update(EquipmentPlacementContract equipmentPlacemntContract);
    Task Delete(EquipmentPlacementContract equipmentPlacemntContract);
    Task<EquipmentPlacementContract> GetById(int id);
    Task<IQueryable<EquipmentPlacementContract>> GetAll();
    Task<IQueryable<EquipmentPlacementContract>> FindBy(Expression<Func<EquipmentPlacementContract, bool>> predicate);
}
