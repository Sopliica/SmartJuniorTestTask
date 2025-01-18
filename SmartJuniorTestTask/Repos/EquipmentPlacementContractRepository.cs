using Microsoft.EntityFrameworkCore;
using SmartJuniorTestTask.Db;
using SmartJuniorTestTask.Models;
using SmartJuniorTestTask.Repos.Interfaces;
using System.Linq.Expressions;

namespace SmartJuniorTestTask.Repos;

public class EquipmentPlacementContractRepository : IEquipmentPlacemntContractRepository
{
    private readonly MsSqlDbContext _context;
    private readonly DbSet<EquipmentPlacementContract> _equipmentPlacementContractSet;
    public EquipmentPlacementContractRepository(MsSqlDbContext context)
    {
        _context = context;
        _equipmentPlacementContractSet = context.Set<EquipmentPlacementContract>();
    }
    public async Task<EquipmentPlacementContract> Create(EquipmentPlacementContract equipmentPlacemntContract)
    {
        await _equipmentPlacementContractSet.AddAsync(equipmentPlacemntContract);
        await Save();
        return equipmentPlacemntContract;
    }

    public async Task Delete(EquipmentPlacementContract equipmentPlacemntContract)
    {
        _equipmentPlacementContractSet.Remove(equipmentPlacemntContract);
        await Save();
    }

    public async Task<IQueryable<EquipmentPlacementContract>> FindBy(Expression<Func<EquipmentPlacementContract, bool>> predicate)
    {
        return _equipmentPlacementContractSet.Where(predicate);
    }

    public async Task<IQueryable<EquipmentPlacementContract>> GetAll()
    {
        return _equipmentPlacementContractSet;
    }

    public async Task<EquipmentPlacementContract> GetById(int id)
    {
        return await _equipmentPlacementContractSet.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<EquipmentPlacementContract> Update(EquipmentPlacementContract equipmentPlacemntContract)
    {
        _equipmentPlacementContractSet.Update(equipmentPlacemntContract);
        await Save();
        return equipmentPlacemntContract;
    }

    private async Task Save()
    {
        await _context.SaveChangesAsync();
    }
}
