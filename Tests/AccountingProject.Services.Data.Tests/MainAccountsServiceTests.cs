namespace AccountingProject.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AccountingProject.Data;
    using AccountingProject.Data.Common.Repositories;
    using AccountingProject.Data.Models;
    using AccountingProject.Data.Repositories;
    using AccountingProject.Services.Mapping;
    using AccountingProject.Web.ViewModels.GLAccounts;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    public class MainAccountsServiceTests
    {
        [Fact]
        public void GetAllWithTurnoverForPeriodShouldReturnCorrectResult()
        {
            var list = new List<GLAccount>();
            var input = new GLAccount
            {
                Name = "Test",
                Code = 205,
            };
            list.Add(input);
            var mockRepoMain = new Mock<IDeletableEntityRepository<GLAccount>>();
            var mockRepoAnalyt = new Mock<IDeletableEntityRepository<AnalyticalAccount>>();
            mockRepoMain.Setup(x => x.All()).Returns(list.AsQueryable());
            var service = new MainAccountsService(mockRepoMain.Object, mockRepoAnalyt.Object);

            var actualResult = service.AllWithTurnoverForPeriod(new DateTime(2020, 1, 1), new DateTime(2020, 12, 31));
            var expectedResult = new List<TrialBalanceAccountViewModel>
            {
                new TrialBalanceAccountViewModel
                {
                    Name = "Test",
                    Code = 205,
                },
            };

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public async Task CreateShouldAddNewAccountCorrectly()
        {
            var list = new List<GLAccount>();
            var mockRepoMain = new Mock<IDeletableEntityRepository<GLAccount>>();
            var mockRepoAnalyt = new Mock<IDeletableEntityRepository<AnalyticalAccount>>();
            mockRepoMain.Setup(x => x.AddAsync(It.IsAny<GLAccount>())).Callback(
                (GLAccount mainAccount) => list.Add(mainAccount));
            var service = new MainAccountsService(mockRepoMain.Object, mockRepoAnalyt.Object);
            var input = new CreateMainAccountInputModel
            {
                Name = "Goods",
                Code = 304,
                IsInventory = true,
                IsFixedAsset = false,
            };
            await service.CreateAsync(input);
            var expectedName = list.First().Name;
            var actualName = "Goods";
            var expectedCode = list.First().Code;
            var expectedIsInventory = list.First().IsInventory;
            var expectedIsFixedAsset = list.First().IsFixedAsset;

            Assert.Single(list);
            Assert.Equal(actualName, expectedName);
            Assert.Equal(304, expectedCode);
            Assert.True(expectedIsInventory);
            Assert.False(expectedIsFixedAsset);
        }

        [Fact]
        public void GetAllShouldReturnCorrectResult()
        {
            var list = new List<GLAccount>
            {
                new GLAccount
                {
                    Id = 5,
                    Name = "Goods",
                    Code = 304,
                    IsInventory = true,
                    IsFixedAsset = false,
                    DebitBalance = 50,
                    CreditBalance = 0,
                },
            };
            var mockRepoMain = new Mock<IDeletableEntityRepository<GLAccount>>();
            mockRepoMain.Setup(x => x.AllAsNoTracking()).Returns(list.AsQueryable());
            var mockRepoAnalyt = new Mock<IDeletableEntityRepository<AnalyticalAccount>>();
            var service = new MainAccountsService(mockRepoMain.Object, mockRepoAnalyt.Object);
            AutoMapperConfig.RegisterMappings(typeof(MainAccountViewModel).Assembly);
            var actualResult = service.GetAll<MainAccountViewModel>().ToList();
            var expectedResult = new List<MainAccountViewModel>
            {
                new MainAccountViewModel
                {
                    Id = 5,
                    Name = "Goods",
                    Code = 304,
                    IsInventory = true,
                    IsFixedAsset = false,
                    DebitBalance = 50,
                    CreditBalance = 0,
                },
            };

            Assert.Single(actualResult);
            Assert.Equal(expectedResult.First().Name, actualResult.First().Name);
            Assert.Equal(expectedResult.First().Id, actualResult.First().Id);
            Assert.Equal(expectedResult.First().Code, actualResult.First().Code);
            Assert.Equal(expectedResult.First().IsFixedAsset, actualResult.First().IsFixedAsset);
            Assert.Equal(expectedResult.First().IsInventory, actualResult.First().IsInventory);
            Assert.Equal(expectedResult.First().DebitBalance, actualResult.First().DebitBalance);
            Assert.Equal(expectedResult.First().CreditBalance, actualResult.First().CreditBalance);
        }

        [Fact]
        public async Task DeleteAsyncShouldCorrectlyDeleteGLAccount()
        {
            var mainAccount = new GLAccount
            {
                Id = 5,
                Name = "Goods",
                Code = 304,
                IsInventory = true,
                IsFixedAsset = false,
                DebitBalance = 50,
                CreditBalance = 0,
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            var repo = new EfDeletableEntityRepository<GLAccount>(dbContext);
            var repoAnalytics = new EfDeletableEntityRepository<AnalyticalAccount>(dbContext);
            await repo.AddAsync(mainAccount);
            await repo.SaveChangesAsync();
            var service = new MainAccountsService(repo, repoAnalytics);
            await service.DeleteAsync(mainAccount.Id);
            var mainAccounts = repo.AllAsNoTracking().ToList();

            Assert.Empty(mainAccounts);
        }

        [Fact]
        public async Task UpdateAsyncShouldCorrectlyUpdateGLAccount()
        {
            var mainAccount = new GLAccount
            {
                Id = 1,
                Name = "Goods",
                Code = 304,
                IsInventory = true,
                IsFixedAsset = false,
                DebitBalance = 50,
                CreditBalance = 0,
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            var repo = new EfDeletableEntityRepository<GLAccount>(dbContext);
            var repoAnalytics = new EfDeletableEntityRepository<AnalyticalAccount>(dbContext);
            await repo.AddAsync(mainAccount);
            await repo.SaveChangesAsync();
            var service = new MainAccountsService(repo, repoAnalytics);
            var input = new EditMainAccountInputModel
            {
                Id = 1,
                Name = "Machines",
                Code = 204,
                IsInventory = false,
                IsFixedAsset = true,
            };
            await service.UpdateAsync(1, input);

            Assert.Equal("Machines", mainAccount.Name);
            Assert.Equal(204, mainAccount.Code);
            Assert.True(mainAccount.IsFixedAsset);
            Assert.False(mainAccount.IsInventory);
        }
    }
}
