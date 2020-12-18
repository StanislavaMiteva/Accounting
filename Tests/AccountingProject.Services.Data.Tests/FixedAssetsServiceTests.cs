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
    using AccountingProject.Web.ViewModels.FixedAssets;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    public class FixedAssetsServiceTests
    {
        [Fact]
        public async Task CreateShouldAddNewFixedAssetCorrectly()
        {
            var list = new List<FixedAsset>();
            var mockRepo = new Mock<IDeletableEntityRepository<FixedAsset>>();
            mockRepo.Setup(x => x.AddAsync(It.IsAny<FixedAsset>())).Callback(
                (FixedAsset fixedAsset) => list.Add(fixedAsset));
            var service = new FixedAssetsService(mockRepo.Object);
            var input = new CreateFixedAssetInputModel
            {
                Name = "Test",
                InventoryNumber = "125878a5",
                Quantity = 1,
                AcquisitionPrice = 15,
                AcquisitionDate = new DateTime(2020, 1, 5),
                DerecognitionDate = new DateTime(2020, 1, 1),
                UsefulLife = 4,
                SalvageValue = 0,
                AccountablePerson = "AccountablePerson",
                MainAccountId = 5,
            };
            await service.CreateAsync(input);
            var name = list.First().Name;
            var inventoryNumber = list.First().InventoryNumber;
            var quantity = list.First().Quantity;
            var acquisitionPrice = list.First().AcquisitionPrice;
            var acquisitionDate = list.First().AcquisitionDate;
            var derecognitionDate = list.First().DerecognitionDate;
            var usefulLife = list.First().UsefulLife;
            var salvageValue = list.First().SalvageValue;
            var accountablePerson = list.First().AccountablePerson;
            var mainAccountId = list.First().GLAccountId;

            Assert.Single(list);
            Assert.Equal("Test", name);
            Assert.Equal("125878a5", inventoryNumber);
            Assert.Equal(1, quantity);
            Assert.Equal(15, acquisitionPrice);
            Assert.Equal(new DateTime(2020, 1, 5), acquisitionDate);
            Assert.Equal(new DateTime(2020, 1, 1), derecognitionDate);
            Assert.Equal(4, usefulLife);
            Assert.Equal(0, salvageValue);
            Assert.Equal("AccountablePerson", accountablePerson);
            Assert.Equal(5, mainAccountId);
        }

        [Fact]
        public async Task GetAllAsyncShouldReturnCorrectResult()
        {
            var fixedAsset = new FixedAsset
            {
                Id = 1,
                Name = "Test",
                InventoryNumber = "125878a5",
                Quantity = 1,
                AcquisitionPrice = 15,
                AcquisitionDate = new DateTime(2020, 1, 5),
                DerecognitionDate = new DateTime(2020, 1, 1),
                UsefulLife = 4,
                SalvageValue = 0,
                DepreciationMethod = DepreciationMethod.StraightLine,
                AccountablePerson = "AccountablePerson",
                GLAccountId = 5,
                GLAccount = new GLAccount { Id = 5, Name = "AccountName", Code = 503 },
            };
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            var repo = new EfDeletableEntityRepository<FixedAsset>(dbContext);
            var service = new FixedAssetsService(repo);
            await repo.AddAsync(fixedAsset);
            await repo.SaveChangesAsync();
            AutoMapperConfig.RegisterMappings(typeof(FixedAssetViewModel).Assembly);
            var actualResult = await service.GetAllAsync<FixedAssetViewModel>();
            var expectedResult = new List<FixedAssetViewModel>
            {
                new FixedAssetViewModel
                {
                    Id = 1,
                    Name = "Test",
                    InventoryNumber = "125878a5",
                    Quantity = 1,
                    AcquisitionPrice = 15,
                    AcquisitionDate = new DateTime(2020, 1, 5),
                    DerecognitionDate = new DateTime(2020, 1, 1),
                    AccountablePerson = "AccountablePerson",
                    UsefulLife = 4,
                    SalvageValue = 0,
                    DepreciationMethod = DepreciationMethod.StraightLine,
                    GLAccountCode = 503,
                    GLAccountName = "AccountName",
                },
            };

            Assert.Single(actualResult);
            Assert.Equal(expectedResult.First().Name, actualResult.First().Name);
            Assert.Equal(expectedResult.First().Id, actualResult.First().Id);
            Assert.Equal(expectedResult.First().InventoryNumber, actualResult.First().InventoryNumber);
            Assert.Equal(expectedResult.First().Quantity, actualResult.First().Quantity);
            Assert.Equal(expectedResult.First().AcquisitionPrice, actualResult.First().AcquisitionPrice);
            Assert.Equal(expectedResult.First().AcquisitionDate, actualResult.First().AcquisitionDate);
            Assert.Equal(expectedResult.First().DerecognitionDate, actualResult.First().DerecognitionDate);
            Assert.Equal(expectedResult.First().UsefulLife, actualResult.First().UsefulLife);
            Assert.Equal(expectedResult.First().SalvageValue, actualResult.First().SalvageValue);
            Assert.Equal(expectedResult.First().AccountablePerson, actualResult.First().AccountablePerson);
            Assert.Equal(expectedResult.First().GLAccountCode, actualResult.First().GLAccountCode);
            Assert.Equal(expectedResult.First().GLAccountName, actualResult.First().GLAccountName);
            Assert.Equal(expectedResult.First().DepreciationMethod, actualResult.First().DepreciationMethod);
        }

        [Fact]
        public async Task DeleteAsyncShouldCorrectlyDeleteFixedAsset()
        {
            var fixedAsset = new FixedAsset
            {
                Id = 1,
                Name = "Test",
                InventoryNumber = "125878a5",
                Quantity = 1,
                AcquisitionPrice = 15,
                AcquisitionDate = new DateTime(2020, 1, 5),
                DerecognitionDate = new DateTime(2020, 1, 1),
                UsefulLife = 4,
                SalvageValue = 0,
                DepreciationMethod = DepreciationMethod.StraightLine,
                AccountablePerson = "AccountablePerson",
                GLAccountId = 5,
                GLAccount = new GLAccount { Id = 5, Name = "AccountName", Code = 503 },
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            var repo = new EfDeletableEntityRepository<FixedAsset>(dbContext);
            await repo.AddAsync(fixedAsset);
            await repo.SaveChangesAsync();
            var service = new FixedAssetsService(repo);
            await service.DeleteAsync(fixedAsset.Id);
            var fixedAssets = repo.AllAsNoTracking().ToList();

            Assert.Empty(fixedAssets);
        }

        [Fact]
        public async Task UpdateAsyncShouldCorrectlyUpdateFixedAsset()
        {
            var fixedAsset = new FixedAsset
            {
                Id = 1,
                Name = "Test",
                InventoryNumber = "125878a5",
                Quantity = 1,
                AcquisitionPrice = 15,
                AcquisitionDate = new DateTime(2020, 1, 5),
                DerecognitionDate = new DateTime(2020, 1, 1),
                UsefulLife = 4,
                SalvageValue = 0,
                DepreciationMethod = DepreciationMethod.StraightLine,
                AccountablePerson = "AccountablePerson",
                GLAccountId = 5,
                GLAccount = new GLAccount { Id = 5, Name = "AccountName", Code = 503 },
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            var repo = new EfDeletableEntityRepository<FixedAsset>(dbContext);
            await repo.AddAsync(fixedAsset);
            await repo.SaveChangesAsync();
            var service = new FixedAssetsService(repo);
            var input = new EditFixedAssetInputModel
            {
                Id = 1,
                Name = "TestNew",
                InventoryNumber = "125878a5New",
                Quantity = 5,
                AcquisitionPrice = 25,
                AcquisitionDate = new DateTime(2020, 1, 15),
                DerecognitionDate = new DateTime(2020, 1, 11),
                UsefulLife = 1,
                SalvageValue = 50,
                DepreciationMethod = DepreciationMethod.DoubleDecliningBalance,
                AccountablePerson = "AccountablePersonNew",
                MainAccountId = 8,
            };
            await service.UpdateAsync(1, input);

            Assert.Equal("TestNew", fixedAsset.Name);
            Assert.Equal("125878a5New", fixedAsset.InventoryNumber);
            Assert.Equal(5, fixedAsset.Quantity);
            Assert.Equal(25, fixedAsset.AcquisitionPrice);
            Assert.Equal(new DateTime(2020, 1, 15), fixedAsset.AcquisitionDate);
            Assert.Equal(new DateTime(2020, 1, 11), fixedAsset.DerecognitionDate);
            Assert.Equal(1, fixedAsset.UsefulLife);
            Assert.Equal(50, fixedAsset.SalvageValue);
            Assert.Equal(DepreciationMethod.DoubleDecliningBalance, fixedAsset.DepreciationMethod);
            Assert.Equal("AccountablePersonNew", fixedAsset.AccountablePerson);
            Assert.Equal(8, fixedAsset.GLAccountId);
        }
    }
}
