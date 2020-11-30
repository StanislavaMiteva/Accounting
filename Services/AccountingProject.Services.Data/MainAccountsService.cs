namespace AccountingProject.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AccountingProject.Data.Common.Repositories;
    using AccountingProject.Data.Models;
    using AccountingProject.Web.ViewModels.GLAccounts;

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

        public IEnumerable<MainAccountPartViewModel> GetAllOnlyIdCodeName()
        {
            return this.mainAccountsRepository.AllAsNoTracking()
                .Select(x => new MainAccountPartViewModel
                {
                    Id = x.Id,
                    Code = x.Code,
                    Name = x.Name,
                })
                .OrderBy(x => x.Code)
                .ToList();
        }
    }
}
