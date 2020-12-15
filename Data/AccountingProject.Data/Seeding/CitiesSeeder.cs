namespace AccountingProject.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using AccountingProject.Data.Models;

    internal class CitiesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Cities.Any())
            {
                return;
            }

            await dbContext.Cities.AddAsync(new City
            {
                Name = "Sofia",
            });
            await dbContext.Cities.AddAsync(new City
            {
                Name = "Plovdiv",
            });
            await dbContext.Cities.AddAsync(new City
            {
                Name = "Varna",
            });
            await dbContext.SaveChangesAsync();
        }
    }
}
