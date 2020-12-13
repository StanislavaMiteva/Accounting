namespace AccountingProject.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AccountingProject.Data.Common.Repositories;
    using AccountingProject.Data.Models;
    using AccountingProject.Services.Mapping;
    using AccountingProject.Web.ViewModels.Inventories;
    using Microsoft.EntityFrameworkCore;

    public class InventoriesService : IInventoriesService
    {
        private readonly IDeletableEntityRepository<Inventory> inventoriesRepository;

        public InventoriesService(IDeletableEntityRepository<Inventory> inventoriesRepository)
        {
            this.inventoriesRepository = inventoriesRepository;
        }

        public async Task CreateAsync(CreateInventoryInputModel input)
        {
            var inventory = new Inventory
            {
                Name = input.Name,
                Measure = input.Measure,
                Quantity = input.Quantity,
                Price = input.Price,
                GLAccountId = input.MainAccountId,
            };
            await this.inventoriesRepository.AddAsync(inventory);
            await this.inventoriesRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var inventory = await this.inventoriesRepository
                        .All()
                        .FirstOrDefaultAsync(x => x.Id == id);
            this.inventoriesRepository.Delete(inventory);
            await this.inventoriesRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.inventoriesRepository.AllAsNoTracking()
               .OrderBy(x => x.Name)
               .To<T>()
               .ToList();
        }

        public IEnumerable<T> GetAllByAccount<T>(int accountId)
        {
            return this.inventoriesRepository.AllAsNoTracking()
                .Where(x => x.GLAccountId == accountId)
                .OrderBy(x => x.Name)
                .To<T>()
                .ToList();
        }

        public async Task<T> GetByIdAsync<T>(int id)
        {
            return await this.inventoriesRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();
        }

        public async Task<bool> IsNameAvailableAsync(string name)
        {
            return !await this.inventoriesRepository
                .AllAsNoTracking()
                .AnyAsync(x => x.Name == name);
        }

        public async Task UpdateAsync(int id, EditInventoryInputModel input)
        {
            var inventory = await this.inventoriesRepository
                .All()
                .FirstOrDefaultAsync(x => x.Id == id);
            inventory.Name = input.Name;
            inventory.Measure = input.Measure;
            inventory.Quantity = input.Quantity;
            inventory.Price = input.Price;
            inventory.GLAccountId = input.MainAccountId;
            await this.inventoriesRepository.SaveChangesAsync();
        }
    }
}
