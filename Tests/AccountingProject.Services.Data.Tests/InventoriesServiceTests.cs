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
    using AccountingProject.Web.ViewModels.Inventories;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    public class InventoriesServiceTests
    {
        [Fact]
        public async Task CreateShouldAddNewInventoryCorrectly()
        {
            var list = new List<Inventory>();
            var mockRepo = new Mock<IDeletableEntityRepository<Inventory>>();
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Inventory>())).Callback(
                (Inventory inventory) => list.Add(inventory));
            var service = new InventoriesService(mockRepo.Object);
            var input = new CreateInventoryInputModel
            {
                Name = "Name",
                Measure = "l",
                Quantity = 10,
                Price = 5,
                MainAccountId = 1,
            };
            await service.CreateAsync(input);
            var name = list.First().Name;
            var measure = list.First().Measure;
            var quantity = list.First().Quantity;
            var price = list.First().Price;
            var mainAccountId = list.First().GLAccountId;

            Assert.Single(list);
            Assert.Equal("Name", name);
            Assert.Equal("l", measure);
            Assert.Equal(10, quantity);
            Assert.Equal(5, price);
            Assert.Equal(1, mainAccountId);
        }

        [Fact]
        public void GetAllShouldReturnCorrectResult()
        {
            var list = new List<Inventory>
            {
                new Inventory
                {
                    Id = 5,
                    Name = "Name",
                    Measure = "l",
                    Quantity = 10,
                    Price = 5,
                    GLAccountId = 1,
                    GLAccount = new GLAccount {Id = 1, Name = "Goods", Code = 304},
                },
            };
            var mockRepo = new Mock<IDeletableEntityRepository<Inventory>>();
            mockRepo.Setup(x => x.AllAsNoTracking()).Returns(list.AsQueryable());
            var service = new InventoriesService(mockRepo.Object);
            AutoMapperConfig.RegisterMappings(typeof(InventoryViewModel).Assembly);
            var actualResult = service.GetAll<InventoryViewModel>().ToList();
            var expectedResult = new List<InventoryViewModel>
            {
                new InventoryViewModel
                {
                    Id = 5,
                    Name = "Name",
                    Measure = "l",
                    Quantity = 10,
                    Price = 5,
                    GLAccountName = "Goods",
                    GLAccountCode = 304,
                },
            };

            Assert.Single(actualResult);
            Assert.Equal(expectedResult.First().Id, actualResult.First().Id);
            Assert.Equal(expectedResult.First().Name, actualResult.First().Name);
            Assert.Equal(expectedResult.First().Measure, actualResult.First().Measure);
            Assert.Equal(expectedResult.First().Quantity, actualResult.First().Quantity);
            Assert.Equal(expectedResult.First().Price, actualResult.First().Price);
            Assert.Equal(expectedResult.First().GLAccountCode, actualResult.First().GLAccountCode);
            Assert.Equal(expectedResult.First().GLAccountName, actualResult.First().GLAccountName);
        }

        [Fact]
        public async Task DeleteAsyncShouldCorrectlyDeleteInventory()
        {
            var inventory = new Inventory
            {
                Id = 5,
                Name = "Name",
                Measure = "l",
                Quantity = 10,
                Price = 5,
                GLAccountId = 1,
                GLAccount = new GLAccount { Id = 1, Name = "Goods", Code = 304 },
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            var repo = new EfDeletableEntityRepository<Inventory>(dbContext);
            await repo.AddAsync(inventory);
            await repo.SaveChangesAsync();
            var service = new InventoriesService(repo);
            await service.DeleteAsync(inventory.Id);
            var inventories = repo.AllAsNoTracking().ToList();

            Assert.Empty(inventories);
        }

        [Fact]
        public async Task UpdateAsyncShouldCorrectlyUpdateInventory()
        {
            var inventory = new Inventory
            {
                Id = 1,
                Name = "Name",
                Measure = "l",
                Quantity = 10,
                Price = 5,
                GLAccountId = 1,
                GLAccount = new GLAccount { Id = 1, Name = "Goods", Code = 304 },
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            var repo = new EfDeletableEntityRepository<Inventory>(dbContext);
            await repo.AddAsync(inventory);
            await repo.SaveChangesAsync();
            var service = new InventoriesService(repo);
            var input = new EditInventoryInputModel
            {
                Id = 1,
                Name = "NameNew",
                Measure = "kg",
                Quantity = 100,
                Price = 0.50M,
                MainAccountId = 3,
            };
            await service.UpdateAsync(1, input);

            Assert.Equal("NameNew", inventory.Name);
            Assert.Equal("kg", inventory.Measure);
            Assert.Equal(100, inventory.Quantity);
            Assert.Equal(0.5M, inventory.Price);
            Assert.Equal(3, inventory.GLAccountId);
        }
    }
}
