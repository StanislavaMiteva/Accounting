﻿namespace AccountingProject.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AccountingProject.Web.ViewModels.GLAccounts;

    public interface IMainAccountsService
    {
        Task CreateAsync(CreateMainAccountInputModel input);

        IEnumerable<T> GetAll<T>();

        Task<bool> IsNameAvailableAsync(string name);

        Task<bool> IsCodeAvailableAsync(int code);

        Task InputBalanceAsync(AddAccountBalanceInputModel input);

        IEnumerable<TrialBalanceAccountViewModel> AllWithTurnoverForPeriod(DateTime startDate, DateTime endDate);
    }
}
