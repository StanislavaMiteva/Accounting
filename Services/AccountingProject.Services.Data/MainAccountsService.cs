namespace AccountingProject.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AccountingProject.Data.Common.Repositories;
    using AccountingProject.Data.Models;
    using AccountingProject.Services.Mapping;
    using AccountingProject.Web.ViewModels.GLAccounts;
    using Microsoft.EntityFrameworkCore;

    public class MainAccountsService : IMainAccountsService
    {
        private readonly IDeletableEntityRepository<GLAccount> mainAccountsRepository;
        private readonly IDeletableEntityRepository<AnalyticalAccount> analyticalAccountsRepository;

        public MainAccountsService(
            IDeletableEntityRepository<GLAccount> mainAccountsRepository,
            IDeletableEntityRepository<AnalyticalAccount> analyticalAccountsRepository)
        {
            this.mainAccountsRepository = mainAccountsRepository;
            this.analyticalAccountsRepository = analyticalAccountsRepository;
        }

        public IEnumerable<TrialBalanceAccountViewModel> AllWithTurnoverForPeriod(DateTime startDate, DateTime endDate)
        {
            var allMainAccounts = this.mainAccountsRepository.All()
                .Select(x => new TrialBalanceAccountViewModel
                {
                    Code = x.Code,
                    Name = x.Name,
                    BeginingDebitBalance = x.DebitBalance,
                    BeginingCreditBalance = x.CreditBalance,
                    DebitTurnoverBeforePeriod = x.DebitTransactions
                        .Where(t => t.DocumentDate < startDate)
                        .Sum(t => t.Amount),
                    CreditTurnoverBeforePeriod = x.CreditTransactions
                        .Where(t => t.DocumentDate < startDate)
                        .Sum(t => t.Amount),
                    DebitTurnoverForPeriod = x.DebitTransactions
                        .Where(t => t.DocumentDate >= startDate && t.DocumentDate <= endDate)
                        .Sum(t => t.Amount),
                    CreditTurnoverForPeriod = x.CreditTransactions
                        .Where(t => t.DocumentDate >= startDate && t.DocumentDate <= endDate)
                        .Sum(t => t.Amount),
                })
                .OrderBy(x => x.Code)
                .ToList();

            foreach (var account in allMainAccounts)
            {
                var startBalance = account.BeginingDebitBalance
                    - account.BeginingCreditBalance
                    + account.DebitTurnoverBeforePeriod
                    - account.CreditTurnoverBeforePeriod;
                if (startBalance > 0)
                {
                    account.StartDebitBalance = startBalance;
                }
                else
                {
                    account.StartCreditBalance = -startBalance;
                }

                var endBalance = account.StartDebitBalance
                    - account.StartCreditBalance
                    + account.DebitTurnoverForPeriod
                    - account.CreditTurnoverForPeriod;
                if (endBalance > 0)
                {
                    account.EndDebitBalance = endBalance;
                }
                else
                {
                    account.EndCreditBalance = -endBalance;
                }
            }

            return allMainAccounts;
        }

        public async Task CreateAsync(CreateMainAccountInputModel input)
        {
            var account = new GLAccount
            {
                Name = input.Name,
                Code = input.Code,
                IsInventory = input.IsInventory,
                IsFixedAsset = input.IsFixedAsset,
            };

            await this.mainAccountsRepository.AddAsync(account);
            await this.mainAccountsRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.mainAccountsRepository.AllAsNoTracking()
                .OrderBy(x => x.Code)
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> GetInventoryAccounts<T>()
        {
            return this.mainAccountsRepository.AllAsNoTracking()
                .Where(x => x.IsInventory == true)
                .OrderBy(x => x.Code)
                .To<T>()
                .ToList();
        }

        public async Task InputBalanceAsync(AddAccountBalanceInputModel input)
        {
            var mainAccount = await this.mainAccountsRepository
                .GetByIdWithDeletedAsync(input.DebitMainAccountId);

            if (input.AnalyticalAccountId != null)
            {
                var analyticalAccount = await this.analyticalAccountsRepository
                    .GetByIdWithDeletedAsync(input.AnalyticalAccountId);
                var oldDebitBalance = analyticalAccount.DebitBalance;
                var oldCreditBalance = analyticalAccount.CreditBalance;
                analyticalAccount.DebitBalance = input.DebitBalance;
                analyticalAccount.CreditBalance = input.CreditBalance;
                mainAccount.DebitBalance += input.DebitBalance - oldDebitBalance;
                mainAccount.CreditBalance += input.CreditBalance - oldCreditBalance;
            }
            else
            {
                mainAccount.DebitBalance = input.DebitBalance;
                mainAccount.CreditBalance = input.CreditBalance;
            }

            await this.mainAccountsRepository.SaveChangesAsync();
        }

        public async Task<bool> IsCodeAvailableAsync(int code)
        {
            return !await this.mainAccountsRepository
                .AllAsNoTracking()
                .AnyAsync(x => x.Code == code);
        }

        public async Task<bool> IsNameAvailableAsync(string name)
        {
            return !await this.mainAccountsRepository
                .AllAsNoTracking()
                .AnyAsync(x => x.Name == name);
        }
    }
}
