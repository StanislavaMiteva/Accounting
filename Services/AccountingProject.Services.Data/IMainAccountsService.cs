namespace AccountingProject.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AccountingProject.Web.ViewModels.GLAccounts;

    public interface IMainAccountsService
    {
        Task CreateAsync(CreateMainAccountInputModel input);

        IEnumerable<T> GetAll<T>();

        IEnumerable<T> GetInventoryAccounts<T>();

        Task<bool> IsNameAvailableAsync(string name);

        Task<bool> IsCodeAvailableAsync(int code);

        Task InputBalanceAsync(AddAccountBalanceInputModel input);

        IEnumerable<TrialBalanceAccountViewModel> AllWithTurnoverForPeriod(DateTime startDate, DateTime endDate);

        IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs();

        Task<T> GetByIdAsync<T>(int id);

        Task UpdateAsync(int id, EditMainAccountInputModel input);

        Task DeleteAsync(int id);
    }
}
