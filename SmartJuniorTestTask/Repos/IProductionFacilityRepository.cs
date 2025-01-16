using System.Linq.Expressions;
using SmartJuniorTestTask.Models;

namespace SmartJuniorTestTask.Repos;

public interface IProductionFacilityRepository
{
    Task<ProductionFacility> Create(ProductionFacility productionFacility);
    Task<ProductionFacility> Update(ProductionFacility productionFacility);
    Task Delete(ProductionFacility productionFacility);
    Task<ProductionFacility> GetById(int id);
    Task<IQueryable<ProductionFacility>> GetAll();
    Task<IQueryable<ProductionFacility>> FindBy(Expression<Func<ProductionFacility, bool>> predicate);
}
