﻿namespace AccountingProject.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AccountingProject.Web.ViewModels.AnalyticalAccounts;

    public interface IAnalyticalAccountsService
    {
        Task CreateAsync(CreateAnalyticalAccountInputModel input);

        IEnumerable<T> GetAllByMainAccountId<T>(int mainAccountId);

        IEnumerable<T> GetAll<T>();
    }
}
