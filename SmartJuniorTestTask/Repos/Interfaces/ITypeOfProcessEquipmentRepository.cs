using SmartJuniorTestTask.Models;
using System.Linq.Expressions;

namespace SmartJuniorTestTask.Repos.Interfaces;

public interface ITypeOfProcessEquipmentRepository
{
    Task<TypeOfProcessEquipment> Create(TypeOfProcessEquipment typeOfProcessEquipment);
    Task<TypeOfProcessEquipment> Update(TypeOfProcessEquipment typeOfProcessEquipment);
    Task Delete(TypeOfProcessEquipment typeOfProcessEquipment);
    Task<TypeOfProcessEquipment> GetById(int id);
    Task<IQueryable<TypeOfProcessEquipment>> GetAll();
    Task<IQueryable<TypeOfProcessEquipment>> FindBy(Expression<Func<TypeOfProcessEquipment, bool>> predicate);
}
