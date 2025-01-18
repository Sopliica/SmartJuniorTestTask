using Microsoft.EntityFrameworkCore;
using SmartJuniorTestTask.Models;

namespace SmartJuniorTestTask.Db;

public class MsSqlDbContext : DbContext
{
    public DbSet<ProductionFacility> ProductionFacilities { get; set; }
    public DbSet<EquipmentPlacementContract> EquipmentPlacementContracts { get; set; }
    public MsSqlDbContext(DbContextOptions<MsSqlDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductionFacility>().HasKey(x => x.Code);
        modelBuilder.Entity<EquipmentPlacementContract>().HasKey(x => x.Id);
    }
}
