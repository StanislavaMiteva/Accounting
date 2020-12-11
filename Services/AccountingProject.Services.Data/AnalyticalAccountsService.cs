namespace AccountingProject.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AccountingProject.Data.Common.Repositories;
    using AccountingProject.Data.Models;
    using AccountingProject.Services.Mapping;
    using AccountingProject.Web.ViewModels.AnalyticalAccounts;
    using Microsoft.EntityFrameworkCore;

    public class AnalyticalAccountsService : IAnalyticalAccountsService
    {
        private readonly IDeletableEntityRepository<AnalyticalAccount> analyticalAccountsRepository;

        public AnalyticalAccountsService(IDeletableEntityRepository<AnalyticalAccount> analyticalAccountsRepository)
        {
            this.analyticalAccountsRepository = analyticalAccountsRepository;
        }

        public async Task CreateAsync(CreateAnalyticalAccountInputModel input)
        {
            var account = new AnalyticalAccount
            {
                Name = input.Name,
                GLAccountId = input.MainAccountId,
            };

            await this.analyticalAccountsRepository.AddAsync(account);
            await this.analyticalAccountsRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.analyticalAccountsRepository.AllAsNoTracking()
               .To<T>()
               .ToList();
        }

        public IEnumerable<T> GetAllByMainAccountId<T>(int mainAccountId)
        {
            return this.analyticalAccountsRepository.AllAsNoTracking()
                .Where(x => x.GLAccountId == mainAccountId)
                .To<T>()
                .ToList();
        }

        public async Task<T> GetByIdAsync<T>(int id)
        {
            return await this.analyticalAccountsRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();
        }

        public string GetNameById(int? id)
        {
            var analyticalAccount = this.analyticalAccountsRepository.AllAsNoTracking()
                .FirstOrDefault(x => x.Id == id);

            if (analyticalAccount == null)
            {
                return null;
            }

            return analyticalAccount.Name;
        }

        public async Task<bool> IsNameAvailableAsync(string name, int mainAccountId)
        {
            return !await this.analyticalAccountsRepository
                .AllAsNoTracking()
                .AnyAsync(x => x.Name == name && x.GlAccount.Id == mainAccountId);
        }

        public async Task UpdateAsync(int id, EditAnalyticalAccountInputModel input)
        {
            var analyticalAccount = await this.analyticalAccountsRepository
                .All()
                .FirstOrDefaultAsync(x => x.Id == id);
            analyticalAccount.Id = input.Id;
            analyticalAccount.Name = input.Name;
            await this.analyticalAccountsRepository.SaveChangesAsync();
        }
    }
}
