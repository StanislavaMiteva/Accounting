﻿namespace AccountingProject.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AccountingProject.Web.ViewModels.AnalyticalAccounts;

    public interface IAnalyticalAccountsService
    {
        Task CreateAsync(CreateAnalyticalAccountInputModel input);

        Task<bool> IsNameAvailableAsync(string name, int mainAccountId);

        IEnumerable<T> GetAllByMainAccountId<T>(int mainAccountId);

        IEnumerable<T> GetAll<T>();

        string GetNameById(int? id);

        Task<T> GetByIdAsync<T>(int id);

        Task UpdateAsync(int id, EditAnalyticalAccountInputModel input);

        Task DeleteAsync(int id);
    }
}
