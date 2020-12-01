namespace AccountingProject.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using AccountingProject.Data.Models;

    internal class DocumentTypesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.DocumentTypes.Any())
            {
                return;
            }

            await dbContext.DocumentTypes.AddAsync(new DocumentType
            {
                Name = "Invoice",
            });
            await dbContext.DocumentTypes.AddAsync(new DocumentType
            {
                Name = "Petty cash credit order",
            });
            await dbContext.DocumentTypes.AddAsync(new DocumentType
            {
                Name = "Petty cash payment order",
            });
            await dbContext.DocumentTypes.AddAsync(new DocumentType
            {
                Name = "Bank payment order",
            });
        }
    }
}
