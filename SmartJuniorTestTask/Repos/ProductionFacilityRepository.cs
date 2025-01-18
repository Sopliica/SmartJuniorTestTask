using Microsoft.EntityFrameworkCore;
using SmartJuniorTestTask.Db;
using SmartJuniorTestTask.Models;
using SmartJuniorTestTask.Repos.Interfaces;
using System.Linq.Expressions;

namespace SmartJuniorTestTask.Repos;

public class ProductionFacilityRepository : IProductionFacilityRepository
{
    private readonly MsSqlDbContext _context;
    private readonly DbSet<ProductionFacility> _productionFacilitiesSet;
    public ProductionFacilityRepository(MsSqlDbContext context)
    {
        _context = context;
        _productionFacilitiesSet = context.Set<ProductionFacility>();
    }
    public async Task<ProductionFacility> Create(ProductionFacility productionFacility)
    {
        await _productionFacilitiesSet.AddAsync(productionFacility);
        await Save();
        return productionFacility;
    }

    public async Task Delete(ProductionFacility productionFacility)
    {
        _productionFacilitiesSet.Remove(productionFacility);
        await Save();
    }

    public async Task<IQueryable<ProductionFacility>> FindBy(Expression<Func<ProductionFacility, bool>> predicate)
    {
        return _productionFacilitiesSet.Where(predicate);
    }

    public async Task<IQueryable<ProductionFacility>> GetAll()
    {
        return _productionFacilitiesSet;
    }

    public async Task<ProductionFacility> GetById(int id)
    {
        return await _productionFacilitiesSet.FirstOrDefaultAsync(x => x.Code == id);
    }

    public async Task<ProductionFacility> Update(ProductionFacility productionFacility)
    {
        _productionFacilitiesSet.Update(productionFacility);
        await Save();
        return productionFacility;
    }

    private async Task Save()
    {
        await _context.SaveChangesAsync();
    }
}
