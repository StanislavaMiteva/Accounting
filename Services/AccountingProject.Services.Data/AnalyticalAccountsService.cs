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

        public async Task<bool> IsNameAvailableAsync(string name)
        {
            return !await this.analyticalAccountsRepository
                .AllAsNoTracking()
                .AnyAsync(x => x.Name == name);
        }
    }
}
