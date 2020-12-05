namespace AccountingProject.Services.Data
{
    using System.Threading.Tasks;

    using AccountingProject.Data.Common.Repositories;
    using AccountingProject.Data.Models;
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

        public async Task<bool> IsNameAvailableAsync(string name)
        {
            return !await this.inventoriesRepository
                .AllAsNoTracking()
                .AnyAsync(x => x.Name == name);
        }
    }
}
