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
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    public class AnalyticalAccountsServiceTests
    {
        [Fact]
        public async Task CreateShouldAddNewAccountCorrectly()
        {
            var list = new List<AnalyticalAccount>();
            var mockRepo = new Mock<IDeletableEntityRepository<AnalyticalAccount>>();
            mockRepo.Setup(x => x.AddAsync(It.IsAny<AnalyticalAccount>())).Callback(
                (AnalyticalAccount analyticalAccount) => list.Add(analyticalAccount));
            var service = new AnalyticalAccountsService(mockRepo.Object);
            var input = new CreateAnalyticalAccountInputModel
            {
                Name = "Stationalry",
                MainAccountId = 3,
            };
            await service.CreateAsync(input);
            var name = list.First().Name;
            var mainAccountId = list.First().GLAccountId;

            Assert.Single(list);
            Assert.Equal("Stationalry", name);
            Assert.Equal(3, mainAccountId);
        }

        [Fact]
        public void GetAllShouldReturnCorrectResult()
        {
            var list = new List<AnalyticalAccount>
            {
                new AnalyticalAccount
                {
                    Id = 5,
                    Name = "Stationalry",
                    GLAccountId = 3,
                },
            };
            var mockRepo = new Mock<IDeletableEntityRepository<AnalyticalAccount>>();
            mockRepo.Setup(x => x.AllAsNoTracking()).Returns(list.AsQueryable());
            var service = new AnalyticalAccountsService(mockRepo.Object);
            AutoMapperConfig.RegisterMappings(typeof(AnalyticalAccountPartViewModel).Assembly);
            var actualResult = service.GetAll<AnalyticalAccountPartViewModel>().ToList();
            var expectedResult = new List<AnalyticalAccountPartViewModel>
            {
                new AnalyticalAccountPartViewModel { Id = 5, Name = "Stationalry", },
            };

            Assert.Single(actualResult);
            Assert.Equal(expectedResult.First().Name, actualResult.First().Name);
            Assert.Equal(expectedResult.First().Id, actualResult.First().Id);
        }

        [Fact]
        public void GetAllByMainAccountIdShouldReturnCorrectResult()
        {
            var list = new List<AnalyticalAccount>
            {
                new AnalyticalAccount
                {
                    Id = 5,
                    Name = "Stationalry",
                    GLAccountId = 3,
                },
            };
            var mockRepo = new Mock<IDeletableEntityRepository<AnalyticalAccount>>();
            mockRepo.Setup(x => x.AllAsNoTracking()).Returns(list.AsQueryable());
            var service = new AnalyticalAccountsService(mockRepo.Object);
            AutoMapperConfig.RegisterMappings(typeof(AnalyticalAccountPartViewModel).Assembly);
            var actualResult = service.GetAllByMainAccountId<AnalyticalAccountPartViewModel>(3).ToList();
            var expectedResult = new List<AnalyticalAccountPartViewModel>
            {
                new AnalyticalAccountPartViewModel { Id = 5, Name = "Stationalry", },
            };

            Assert.Single(actualResult);
            Assert.Equal(expectedResult.First().Name, actualResult.First().Name);
            Assert.Equal(expectedResult.First().Id, actualResult.First().Id);
        }

        [Fact]
        public async Task GetByIdAsyncShouldReturnCorrectAccount()
        {
            var account = new AnalyticalAccount
            {
                Id = 5,
                Name = "Stationalry",
                GLAccountId = 3,
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            var repo = new EfDeletableEntityRepository<AnalyticalAccount>(dbContext);

            await repo.AddAsync(account);
            await repo.SaveChangesAsync();
            var service = new AnalyticalAccountsService(repo);
            AutoMapperConfig.RegisterMappings(typeof(AnalyticalAccountPartViewModel).Assembly);
            var actualAccountById = await service.GetByIdAsync<AnalyticalAccountPartViewModel>(account.Id);
            var expectedAccount = new AnalyticalAccountPartViewModel { Id = 5, Name = "Stationalry", };

            Assert.Equal(expectedAccount.Id, actualAccountById.Id);
            Assert.Equal(expectedAccount.Name, actualAccountById.Name);
        }

        [Fact]
        public void GetNameByIdShouldReturnNameIfIdIsNotNull()
        {
            var list = new List<AnalyticalAccount>
            {
                new AnalyticalAccount
                {
                    Id = 5,
                    Name = "Stationalry",
                    GLAccountId = 3,
                },
            };
            var mockRepo = new Mock<IDeletableEntityRepository<AnalyticalAccount>>();
            mockRepo.Setup(x => x.AllAsNoTracking()).Returns(list.AsQueryable());
            var service = new AnalyticalAccountsService(mockRepo.Object);
            AutoMapperConfig.RegisterMappings(typeof(AnalyticalAccountPartViewModel).Assembly);
            var actualResult = service.GetNameById(5);

            Assert.Equal("Stationalry", actualResult);
        }

        [Fact]
        public void GetNameByIdShouldReturnNullIfIdIsNull()
        {
            var list = new List<AnalyticalAccount>
            {
                new AnalyticalAccount
                {
                    Id = 5,
                    Name = "Stationalry",
                    GLAccountId = 3,
                },
            };
            var mockRepo = new Mock<IDeletableEntityRepository<AnalyticalAccount>>();
            mockRepo.Setup(x => x.AllAsNoTracking()).Returns(list.AsQueryable());
            var service = new AnalyticalAccountsService(mockRepo.Object);
            AutoMapperConfig.RegisterMappings(typeof(AnalyticalAccountPartViewModel).Assembly);
            var actualResult = service.GetNameById(null);

            Assert.Null(actualResult);
        }

        [Fact]
        public async Task DeleteAsyncShouldCorrectlyDeleteAccount()
        {
            var account = new AnalyticalAccount
            {
                    Id = 5,
                    Name = "Stationalry",
                    GLAccountId = 3,
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            var repo = new EfDeletableEntityRepository<AnalyticalAccount>(dbContext);

            await repo.AddAsync(account);
            await repo.SaveChangesAsync();
            var service = new AnalyticalAccountsService(repo);
            await service.DeleteAsync(account.Id);
            var accounts = repo.AllAsNoTracking().ToList();

            Assert.Empty(accounts);
        }

        [Fact]
        public async Task UpdateAsyncShouldCorrectlyUpdateAccount()
        {
            var account = new AnalyticalAccount
            {
                Id = 5,
                Name = "Stationary",
                GLAccountId = 3,
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            var repo = new EfDeletableEntityRepository<AnalyticalAccount>(dbContext);
            await repo.AddAsync(account);
            await repo.SaveChangesAsync();
            var service = new AnalyticalAccountsService(repo);
            var input = new EditAnalyticalAccountInputModel
            {
                Id = 5,
                Name = "StationaryNew",
            };
            await service.UpdateAsync(5, input);

            Assert.Equal("StationaryNew", account.Name);
        }

        [Fact]
        public async Task IsNameAvailableAsyncShouldReturnTrueIfNameDoesNotExist()
        {
            var account = new AnalyticalAccount
            {
                Id = 5,
                Name = "Stationary",
                GLAccountId = 3,
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            var repo = new EfDeletableEntityRepository<AnalyticalAccount>(dbContext);

            await repo.AddAsync(account);
            await repo.SaveChangesAsync();
            var service = new AnalyticalAccountsService(repo);
            var actualresult = await service.IsNameAvailableAsync("NewName", 3);

            Assert.True(actualresult);
        }

        [Fact]
        public async Task IsNameAvailableAsyncShouldReturnFalseIfNameExists()
        {
            var account = new AnalyticalAccount
            {
                Id = 5,
                Name = "Stationary",
                GLAccountId = 3,
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            var repo = new EfDeletableEntityRepository<AnalyticalAccount>(dbContext);

            await repo.AddAsync(account);
            await repo.SaveChangesAsync();
            var service = new AnalyticalAccountsService(repo);
            var actualresult = await service.IsNameAvailableAsync(account.Name, account.GLAccountId);

            Assert.False(actualresult);
        }
    }
}
