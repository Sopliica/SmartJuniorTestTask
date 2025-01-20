using Microsoft.EntityFrameworkCore;
using SmartJuniorTestTask.Db;
using SmartJuniorTestTask.Models;
using SmartJuniorTestTask.Repos.Interfaces;
using System.Linq.Expressions;

namespace SmartJuniorTestTask.Repos;

public class TypeOfProcessEquipmentRepository : ITypeOfProcessEquipmentRepository
{
    private readonly MsSqlDbContext _context;
    private readonly DbSet<TypeOfProcessEquipment> _typeOfProcessEquipmentSet;
    public TypeOfProcessEquipmentRepository(MsSqlDbContext context)
    {
        _context = context;
        _typeOfProcessEquipmentSet = context.Set<TypeOfProcessEquipment>();
    }
    public async Task<TypeOfProcessEquipment> Create(TypeOfProcessEquipment typeOfProcessEquipment)
    {
        await _typeOfProcessEquipmentSet.AddAsync(typeOfProcessEquipment);
        await Save();
        return typeOfProcessEquipment;
    }

    public async Task Delete(TypeOfProcessEquipment typeOfProcessEquipment)
    {
        _typeOfProcessEquipmentSet.Remove(typeOfProcessEquipment);
        await Save();
    }

    public async Task<IQueryable<TypeOfProcessEquipment>> FindBy
        (Expression<Func<TypeOfProcessEquipment, bool>> predicate)
    {
        return _typeOfProcessEquipmentSet.Where(predicate);
    }

    public async Task<IQueryable<TypeOfProcessEquipment>> GetAll()
    {
        return _typeOfProcessEquipmentSet;
    }

    public async Task<TypeOfProcessEquipment> GetById(int id)
    {
        return await _typeOfProcessEquipmentSet.FirstOrDefaultAsync(x => x.Code == id);
    }

    public async Task<TypeOfProcessEquipment> Update(TypeOfProcessEquipment typeOfProcessEquipment)
    {
        _typeOfProcessEquipmentSet.Update(typeOfProcessEquipment);
        await Save();
        return typeOfProcessEquipment;
    }

    private async Task Save()
    {
        await _context.SaveChangesAsync();
    }
}
