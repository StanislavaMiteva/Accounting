namespace AccountingProject.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using AccountingProject.Data;
    using AccountingProject.Data.Common.Repositories;
    using AccountingProject.Data.Models;
    using AccountingProject.Data.Repositories;
    using AccountingProject.Services.Data.Tests.TestModels;
    using AccountingProject.Services.Mapping;
    using AccountingProject.Web.ViewModels.AnalyticalAccounts;
    using AccountingProject.Web.ViewModels.Transactions;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    public class TransactionsServiceTests
    {
        [Fact]
        public async Task CreateShouldAddNewTransactionCorrectly()
        {
            var list = new List<Transaction>();
            var mockRepo = new Mock<IDeletableEntityRepository<Transaction>>();
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Transaction>())).Callback(
                (Transaction transaction) => list.Add(transaction));
            var service = new TransactionsService(mockRepo.Object);
            var input = new CreateTransactionInputModel
            {
                IsPurchase = false,
                IsSale = true,
                CounterpartyId = 1,
                DocumentDate = new DateTime(2020, 12, 1),
                CreditAnalyticalAccountId = 5,
                CreditMainAccountId = 6,
                DebitMainAccountId = 8,
                DebitAnalyticalAccountId = 10,
                DocumentTypeId = 1,
                Description = "Description",
                Folder = "Folder",
                ConsecutiveNumber = "ConsecutiveNumber",
                Amount = 100,
            };
            await service.CreateAsync(input);

            Assert.Single(list);
            Assert.False(list.First().IsPurchase);
            Assert.True(list.First().IsSale);
            Assert.Equal(1, list.First().CounterpartyId);
            Assert.Equal(new DateTime(2020, 12, 1), list.First().DocumentDate);
            Assert.Equal(5, list.First().CreditAnalyticalAccountId);
            Assert.Equal(6, list.First().CreditGLAccountId);
            Assert.Equal(8, list.First().DebitGLAccountId);
            Assert.Equal(10, list.First().DebitAnalyticalAccountId);
            Assert.Equal(1, list.First().DocumentTypeId);
            Assert.Equal("Description", list.First().Description);
            Assert.Equal("Folder", list.First().Folder);
            Assert.Equal("ConsecutiveNumber", list.First().ConsecutiveNumber);
            Assert.Equal(100, list.First().Amount);
        }

        [Fact]
        public void GetAllShouldReturnCorrectResult()
        {
            var list = new List<Transaction>
            {
                new Transaction
                {
                    Id = "idNumber",
                    IsPurchase = false,
                    IsSale = true,
                    CounterpartyId = 1,
                    DocumentDate = new DateTime(2020, 12, 1),
                    CreatedOn = new DateTime(2020, 12, 2),
                    CreditAnalyticalAccountId = 5,
                    CreditGLAccountId = 6,
                    DebitGLAccountId = 8,
                    DebitAnalyticalAccountId = 10,
                    DocumentTypeId = 1,
                    DocumentType = new DocumentType { Id = 1, Name = "invoice" },
                    Description = "Description",
                    Folder = "Folder",
                    ConsecutiveNumber = "ConsecutiveNumber",
                    Amount = 100,
                },
            };
            var mockRepo = new Mock<IDeletableEntityRepository<Transaction>>();
            mockRepo.Setup(x => x.AllAsNoTracking()).Returns(list.AsQueryable());
            var service = new TransactionsService(mockRepo.Object);
            AutoMapperConfig.RegisterMappings(typeof(TransactionInListViewModel).Assembly);
            var actualResult = service.GetAll<TransactionInListViewModel>().ToList();
            var expectedResult = new List<TransactionInListViewModel>
            {
                new TransactionInListViewModel
                {
                    Id = "idNumber",
                    DocumentDate = new DateTime(2020, 12, 1),
                    CreatedOn = new DateTime(2020, 12, 2),
                    DocumentTypeName = "invoice",
                    Description = "Description",
                    Folder = "Folder",
                    ConsecutiveNumber = "ConsecutiveNumber",
                    Amount = 100,
                },
            };

            Assert.Single(actualResult);
            Assert.Equal(expectedResult.First().Id, actualResult.First().Id);
            Assert.Equal(expectedResult.First().DocumentDate, actualResult.First().DocumentDate);
            Assert.Equal(expectedResult.First().CreatedOn, actualResult.First().CreatedOn);
            Assert.Equal(expectedResult.First().DocumentTypeName, actualResult.First().DocumentTypeName);
            Assert.Equal(expectedResult.First().Description, actualResult.First().Description);
            Assert.Equal(expectedResult.First().Folder, actualResult.First().Folder);
            Assert.Equal(expectedResult.First().ConsecutiveNumber, actualResult.First().ConsecutiveNumber);
            Assert.Equal(expectedResult.First().Amount, actualResult.First().Amount);
        }

        [Fact]
        public async Task DeleteAsyncShouldCorrectlyDeleteTransaction()
        {
            var transaction = new Transaction
            {
                Id = "idNumber",
                IsPurchase = false,
                IsSale = true,
                CounterpartyId = 1,
                DocumentDate = new DateTime(2020, 12, 1),
                CreatedOn = new DateTime(2020, 12, 2),
                CreditAnalyticalAccountId = 5,
                CreditGLAccountId = 6,
                DebitGLAccountId = 8,
                DebitAnalyticalAccountId = 10,
                DocumentTypeId = 1,
                DocumentType = new DocumentType { Id = 1, Name = "invoice" },
                Description = "Description",
                Folder = "Folder",
                ConsecutiveNumber = "ConsecutiveNumber",
                Amount = 100,
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            var repo = new EfDeletableEntityRepository<Transaction>(dbContext);
            await repo.AddAsync(transaction);
            await repo.SaveChangesAsync();
            var service = new TransactionsService(repo);
            await service.DeleteAsync(transaction.Id);
            var transactions = repo.AllAsNoTracking().ToList();

            Assert.Empty(transactions);
        }

        [Fact]
        public async Task UpdateAsyncShouldCorrectlyUpdateTransaction()
        {
            var transaction = new Transaction
            {
                Id = "idNumber",
                IsPurchase = false,
                IsSale = true,
                CounterpartyId = 1,
                DocumentDate = new DateTime(2020, 12, 1),
                CreatedOn = new DateTime(2020, 12, 2),
                CreditAnalyticalAccountId = 5,
                CreditGLAccountId = 6,
                DebitGLAccountId = 8,
                DebitAnalyticalAccountId = 10,
                DocumentTypeId = 1,
                DocumentType = new DocumentType { Id = 1, Name = "invoice" },
                Description = "Description",
                Folder = "Folder",
                ConsecutiveNumber = "ConsecutiveNumber",
                Amount = 100,
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            var repo = new EfDeletableEntityRepository<Transaction>(dbContext);
            await repo.AddAsync(transaction);
            await repo.SaveChangesAsync();
            var service = new TransactionsService(repo);
            var input = new EditTransactionInputModel
            {
                Id = "idNumber",
                IsPurchase = true,
                IsSale = false,
                CounterpartyId = 2,
                DocumentDate = new DateTime(2020, 12, 11),
                CreditAnalyticalAccountId = 6,
                CreditMainAccountId = 7,
                DebitMainAccountId = 9,
                DebitAnalyticalAccountId = 11,
                DocumentTypeId = 3,
                Description = "DescriptionNew",
                Folder = "FolderNew",
                ConsecutiveNumber = "ConsecutiveNumberNew",
                Amount = 1000,
            };
            await service.UpdateAsync("idNumber", input);

            Assert.True(transaction.IsPurchase);
            Assert.False(transaction.IsSale);
            Assert.Equal(2, transaction.CounterpartyId);
            Assert.Equal(new DateTime(2020, 12, 11), transaction.DocumentDate);
            Assert.Equal(6, transaction.CreditAnalyticalAccountId);
            Assert.Equal(7, transaction.CreditGLAccountId);
            Assert.Equal(9, transaction.DebitGLAccountId);
            Assert.Equal(11, transaction.DebitAnalyticalAccountId);
            Assert.Equal(3, transaction.DocumentTypeId);
            Assert.Equal("DescriptionNew", transaction.Description);
            Assert.Equal("FolderNew", transaction.Folder);
            Assert.Equal("ConsecutiveNumberNew", transaction.ConsecutiveNumber);
            Assert.Equal(1000, transaction.Amount);
        }
    }
}
