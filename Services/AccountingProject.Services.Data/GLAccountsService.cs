namespace AccountingProject.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AccountingProject.Data.Common.Repositories;
    using AccountingProject.Data.Models;
    using AccountingProject.Web.ViewModels.GLAccounts;

    public class GLAccountsService : IGLAccountsService
    {
        private readonly IDeletableEntityRepository<GLAccount> gLAccountsRepository;

        public GLAccountsService(IDeletableEntityRepository<GLAccount> glAccountsRepository)
        {
            this.gLAccountsRepository = glAccountsRepository;
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

            await this.gLAccountsRepository.AddAsync(account);
            await this.gLAccountsRepository.SaveChangesAsync();
        }

        public IEnumerable<MainAccountPartViewModel> GetAllOnlyIdCodeName()
        {
            return this.gLAccountsRepository.AllAsNoTracking()
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
