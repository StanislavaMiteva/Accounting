namespace AccountingProject.Services.Data
{
    using System.Threading.Tasks;

    using AccountingProject.Web.ViewModels.AnalyticalAccounts;

    public interface IAnalyticalAccountsService
    {
        // IEnumerable<AnalyticalAccountViewModel> GetAllAnalyticalAccounts();
        Task CreateAsync(CreateAnalyticalAccountInputModel input);

        // IEnumerable<AnalyticalAccountViewModel> GetAnalyticalAccountsByGLAccountCode(int gLAccountCode);
    }
}
