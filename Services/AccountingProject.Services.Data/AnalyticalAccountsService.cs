namespace AccountingProject.Services.Data
{
    using System.Threading.Tasks;

    using AccountingProject.Data.Common.Repositories;
    using AccountingProject.Data.Models;
    using AccountingProject.Web.ViewModels.AnalyticalAccounts;

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
    }
}
