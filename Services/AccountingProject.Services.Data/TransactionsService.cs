namespace AccountingProject.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AccountingProject.Data.Common.Repositories;
    using AccountingProject.Data.Models;
    using AccountingProject.Services.Mapping;
    using AccountingProject.Web.ViewModels.Shared;
    using AccountingProject.Web.ViewModels.Transactions;

    public class TransactionsService : ITransactionsService
    {
        private readonly IDeletableEntityRepository<Transaction> transactionsRepository;

        public TransactionsService(IDeletableEntityRepository<Transaction> transactionsRepository)
        {
            this.transactionsRepository = transactionsRepository;
        }

        public async Task CreateAsync(CreateTransactionInputModel input)
        {
            var transaction = new Transaction
            {
                IsPurchase = input.IsPurchase,
                IsSale = input.IsSale,
                CounterpartyId = input.CounterpartyId,
                CreatorId = input.CreatorId,
                DocumentDate = input.DocumentDate,
                CreditAnalyticalAccountId = input.CreditAnalyticalAccountId,
                CreditGLAccountId = input.CreditMainAccountId,
                DebitGLAccountId = input.DebitMainAccountId,
                DebitAnalyticalAccountId = input.DebitAnalyticalAccountId,
                DocumentTypeId = input.DocumentTypeId,
                Description = input.Description,
                Folder = input.Folder,
                ConsecutiveNumber = input.ConsecutiveNumber,
                Amount = input.Amount,
            };

            await this.transactionsRepository.AddAsync(transaction);
            await this.transactionsRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var transaction = await this.transactionsRepository
                        .GetByIdWithDeletedAsync(id);
            this.transactionsRepository.Delete(transaction);
            await this.transactionsRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.transactionsRepository.AllAsNoTracking()
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> GetAllTransactionsByMonth<T>(InputYearMonthModel input)
        {
            DateTime startDate = new DateTime(input.Year, input.MonthStart, 01);
            int daysInMonth = DateTime.DaysInMonth(input.Year, input.MonthEnd);
            DateTime endDate = new DateTime(input.Year, input.MonthEnd, daysInMonth);
            return this.transactionsRepository.AllAsNoTracking()
                .Where(x => x.DocumentDate >= startDate && x.DocumentDate <= endDate)
                .OrderBy(x => x.DocumentDate)
                .To<T>()
                .ToList();
        }
    }
}
