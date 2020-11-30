namespace AccountingProject.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AccountingProject.Web.ViewModels.GLAccounts;

    public interface IMainAccountsService
    {
        Task CreateAsync(CreateMainAccountInputModel input);

        IEnumerable<MainAccountPartViewModel> GetAllOnlyIdCodeName();

        // IEnumerable<GLAccountViewModel> GetAllGLAccounts();

        // void InputBalance(InputAccountBalance input);

        // IEnumerable<GLViewModel> AllByMonth(DateTime startDate, DateTime endDate);
    }
}
