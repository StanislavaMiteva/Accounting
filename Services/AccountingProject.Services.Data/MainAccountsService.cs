namespace AccountingProject.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AccountingProject.Data.Common.Repositories;
    using AccountingProject.Data.Models;
    using AccountingProject.Services.Mapping;
    using AccountingProject.Web.ViewModels.GLAccounts;
    using Microsoft.EntityFrameworkCore;

    public class MainAccountsService : IMainAccountsService
    {
        private readonly IDeletableEntityRepository<GLAccount> mainAccountsRepository;

        public MainAccountsService(IDeletableEntityRepository<GLAccount> mainAccountsRepository)
        {
            this.mainAccountsRepository = mainAccountsRepository;
        }

        public async Task CreateAsync(CreateMainAccountInputModel input)
        {
            var account = new GLAccount
            {
                Name = input.Name,
                Code = input.Code,
                IsInventory = input.IsInventory,
                IsFixedAsset = input.IsFixedAsset,
            };

            await this.mainAccountsRepository.AddAsync(account);
            await this.mainAccountsRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.mainAccountsRepository.AllAsNoTracking()
                .To<T>()
                .ToList();
        }

        public async Task<bool> IsCodeAvailableAsync(int code)
        {
            return !await this.mainAccountsRepository
                .AllAsNoTracking()
                .AnyAsync(x => x.Code == code);
        }

        public async Task<bool> IsNameAvailableAsync(string name)
        {
            return !await this.mainAccountsRepository
                .AllAsNoTracking()
                .AnyAsync(x => x.Name == name);
        }
    }
}
