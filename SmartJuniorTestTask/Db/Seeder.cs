using Microsoft.EntityFrameworkCore;
using SmartJuniorTestTask.Models;

namespace SmartJuniorTestTask.Db;

public class Seeder
{
    public static async Task Seed(MsSqlDbContext context)
    {
        await context.Database.MigrateAsync();

        if (!await context.ProductionFacilities.AnyAsync())
        {
            List<ProductionFacility> facilities = new()
            {
                new ProductionFacility()
                {
                    Name = "Katowice Factory",
                    StandardAreaForEquipment = 1000
                },
                new ProductionFacility()
                {
                    Name = "Krakow Warehouse",
                    StandardAreaForEquipment = 5000
                },
                new ProductionFacility()
                {
                    Name = "Bielsko Warehouse",
                    StandardAreaForEquipment = 7000
                }
            };

            await context.AddRangeAsync(facilities);
        }

        if (!await context.TypeOfProcessEquipment.AnyAsync())
        {
            List<TypeOfProcessEquipment> typeOfProcessEquipment = new()
            {
                new TypeOfProcessEquipment()
                {
                    Name = "Lathe",
                    Area = 2000
                },
                new TypeOfProcessEquipment()
                {
                    Name = "Automaton",
                    Area = 100
                },
                new TypeOfProcessEquipment()
                {
                    Name = "Robot",
                    Area = 1000
                }
            };

            await context.AddRangeAsync(typeOfProcessEquipment);
        }

        await context.SaveChangesAsync();
    }
}
