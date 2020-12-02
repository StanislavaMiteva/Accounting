namespace AccountingProject.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AccountingProject.Data.Common.Repositories;
    using AccountingProject.Data.Models;
    using AccountingProject.Web.ViewModels.AnalyticalAccounts;
    using AccountingProject.Services.Mapping;

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

        public IEnumerable<AnalyticalAccountPartViewModel> GetAllOnlyIdName()
        {
            return this.analyticalAccountsRepository.AllAsNoTracking()
                .Select(x => new AnalyticalAccountPartViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .OrderBy(x => x.Name)
                .ToList();
        }

        public IEnumerable<AnalyticalAccountPartViewModel> GetAnalyticalAccountsByMainAccountId(int mainAccountId)
        {
            return this.analyticalAccountsRepository.AllAsNoTracking()
                .Where(x => x.GLAccountId == mainAccountId)
                .Select(x => new AnalyticalAccountPartViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .OrderBy(x => x.Name)
                .ToList();
        }
    }
}
