namespace AccountingProject.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using AccountingProject.Data.Common.Repositories;
    using AccountingProject.Data.Models;
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
                CreatedOn = DateTime.UtcNow,
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
    }
}
