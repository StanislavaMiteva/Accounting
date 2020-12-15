namespace AccountingProject.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using AccountingProject.Data.Models;

    internal class MainAccountsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.GLAccounts.Any())
            {
                return;
            }

            await dbContext.GLAccounts.AddAsync(new GLAccount
            {
                Code = 101,
                Name = "Capital share , registered",
            });
            await dbContext.GLAccounts.AddAsync(new GLAccount
            {
                Code = 123,
                Name = "Current period profit/ loss",
            });
            await dbContext.GLAccounts.AddAsync(new GLAccount
            {
                Code = 203,
                Name = "Buildings, constructions",
                IsFixedAsset = true,
            });
            await dbContext.GLAccounts.AddAsync(new GLAccount
            {
                Code = 204,
                Name = "Machines, equpment",
                IsFixedAsset = true,
            });
            await dbContext.GLAccounts.AddAsync(new GLAccount
            {
                Code = 302,
                Name = "Materials, stocks",
                IsInventory = true,
            });
            await dbContext.GLAccounts.AddAsync(new GLAccount
            {
                Code = 304,
                Name = "Goods or release",
                IsInventory = true,
            });
            await dbContext.GLAccounts.AddAsync(new GLAccount
            {
                Code = 401,
                Name = "Suppliers",
            });
            await dbContext.GLAccounts.AddAsync(new GLAccount
            {
                Code = 411,
                Name = "Customers (accounts receivables)",
            });
            await dbContext.GLAccounts.AddAsync(new GLAccount
            {
                Code = 421,
                Name = "Personnel",
            });
            await dbContext.GLAccounts.AddAsync(new GLAccount
            {
                Code = 501,
                Name = "Cash in hand",
            });
            await dbContext.GLAccounts.AddAsync(new GLAccount
            {
                Code = 503,
                Name = "Bank current account",
            });
            await dbContext.GLAccounts.AddAsync(new GLAccount
            {
                Code = 601,
                Name = "Cost for materials",
            });
            await dbContext.GLAccounts.AddAsync(new GLAccount
            {
                Code = 602,
                Name = "Costs for hired services",
            });
            await dbContext.GLAccounts.AddAsync(new GLAccount
            {
                Code = 603,
                Name = "Depreciation costs",
            });
            await dbContext.GLAccounts.AddAsync(new GLAccount
            {
                Code = 604,
                Name = "Costs for salaries and wages",
            });
            await dbContext.GLAccounts.AddAsync(new GLAccount
            {
                Code = 605,
                Name = "Social insurances and premiums",
            });
            await dbContext.GLAccounts.AddAsync(new GLAccount
            {
                Code = 609,
                Name = "Other expenses",
            });
            await dbContext.GLAccounts.AddAsync(new GLAccount
            {
                Code = 621,
                Name = "Interests’ expenses",
            });
            await dbContext.GLAccounts.AddAsync(new GLAccount
            {
                Code = 702,
                Name = "Incomes from sells of merchandise",
            });
            await dbContext.GLAccounts.AddAsync(new GLAccount
            {
                Code = 709,
                Name = "Other incomes",
            });
            await dbContext.GLAccounts.AddAsync(new GLAccount
            {
                Code = 721,
                Name = "Interest earned",
            });
            await dbContext.SaveChangesAsync();
        }
    }
}
