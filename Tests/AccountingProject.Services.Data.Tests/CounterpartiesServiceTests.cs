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
    using AccountingProject.Web.ViewModels.Counterparties;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    public class CounterpartiesServiceTests
    {
        [Fact]
        public async Task CreateShouldAddNewCounterpartyCorrectly()
        {
            var counterpartiesList = new List<Counterparty>();
            var citiesList = new List<City>
            {
                new City { Id = 1, Name = "Sofia", },
            };
            var mockRepoCounterparty = new Mock<IDeletableEntityRepository<Counterparty>>();
            mockRepoCounterparty.Setup(x => x.AddAsync(It.IsAny<Counterparty>())).Callback(
                (Counterparty counterparty) => counterpartiesList.Add(counterparty));
            var mockRepoCity = new Mock<IDeletableEntityRepository<City>>();
            mockRepoCity.Setup(x => x.AddAsync(It.IsAny<City>())).Callback(
                (City city) => citiesList.Add(city));
            mockRepoCity.Setup(x => x.All()).Returns(citiesList.AsQueryable());

            var service = new CounterpartiesService(mockRepoCounterparty.Object, mockRepoCity.Object);

            var input = new CreateCounterpartyInputModel
            {
                Name = "Test",
                VAT = "888888888888888888",
                Address = "Address",
                CityName = "Sofia",
            };
            await service.CreateAsync(input);
            var name = counterpartiesList.First().Name;
            var vat = counterpartiesList.First().VAT;
            var address = counterpartiesList.First().Address;
            var cityName = counterpartiesList.First().City.Name;

            Assert.Single(counterpartiesList);
            Assert.Equal("Test", name);
            Assert.Equal("888888888888888888", vat);
            Assert.Equal("Address", address);
            Assert.Equal("Sofia", cityName);
        }

        [Fact]
        public async Task CreateShouldAddNewCounterpartyCorrectlyWhenCityNameIsNew()
        {
            var counterpartiesList = new List<Counterparty>();
            var citiesList = new List<City>
            {
                new City { Id = 1, Name = "Sofia", },
            };
            var mockRepoCounterparty = new Mock<IDeletableEntityRepository<Counterparty>>();
            mockRepoCounterparty.Setup(x => x.AddAsync(It.IsAny<Counterparty>())).Callback(
                (Counterparty counterparty) => counterpartiesList.Add(counterparty));
            var mockRepoCity = new Mock<IDeletableEntityRepository<City>>();
            mockRepoCity.Setup(x => x.AddAsync(It.IsAny<City>())).Callback(
                (City city) => citiesList.Add(city));
            mockRepoCity.Setup(x => x.All()).Returns(citiesList.AsQueryable());

            var service = new CounterpartiesService(mockRepoCounterparty.Object, mockRepoCity.Object);

            var input = new CreateCounterpartyInputModel
            {
                Name = "Test",
                VAT = "888888888888888888",
                Address = "Address",
                CityName = "Plovdiv",
            };
            await service.CreateAsync(input);
            var name = counterpartiesList.First().Name;
            var vat = counterpartiesList.First().VAT;
            var address = counterpartiesList.First().Address;
            var cityName = counterpartiesList.First().City.Name;

            Assert.Single(counterpartiesList);
            Assert.Equal("Test", name);
            Assert.Equal("888888888888888888", vat);
            Assert.Equal("Address", address);
            Assert.Equal("Plovdiv", cityName);
        }

        [Fact]
        public void GetAllShouldReturnCorrectResult()
        {
            var list = new List<Counterparty>
            {
                new Counterparty
                {
                    Id = 1,
                    Name = "Test",
                    VAT = "888888888888888888",
                    Address = "Address",
                    City = new City { Id = 1, Name = "Sofia", },
                },
            };
            var mockRepoCounterparty = new Mock<IDeletableEntityRepository<Counterparty>>();
            mockRepoCounterparty.Setup(x => x.AllAsNoTracking()).Returns(list.AsQueryable());
            var mockRepoCity = new Mock<IDeletableEntityRepository<City>>();
            var service = new CounterpartiesService(mockRepoCounterparty.Object, mockRepoCity.Object);

            AutoMapperConfig.RegisterMappings(typeof(CounterpartyPartViewModel).Assembly);
            var actualResult = service.GetAll<CounterpartyPartViewModel>().ToList();
            var expectedResult = new List<CounterpartyPartViewModel>
            {
                new CounterpartyPartViewModel { Id = 1, Name = "Test", },
            };

            Assert.Single(actualResult);
            Assert.Equal(expectedResult.First().Name, actualResult.First().Name);
            Assert.Equal(expectedResult.First().Id, actualResult.First().Id);
        }

        [Fact]
        public async Task DeleteAsyncShouldCorrectlyDeleteCounterparty()
        {
            var counterparty = new Counterparty
            {
                Id = 1,
                Name = "Test",
                VAT = "888888888888888888",
                Address = "Address",
                City = new City { Id = 1, Name = "Sofia", },
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            var repo = new EfDeletableEntityRepository<Counterparty>(dbContext);
            var repoCities = new EfDeletableEntityRepository<City>(dbContext);

            await repo.AddAsync(counterparty);
            await repo.SaveChangesAsync();
            var service = new CounterpartiesService(repo, repoCities);
            await service.DeleteAsync(counterparty.Id);
            var counterparties = repo.AllAsNoTracking().ToList();

            Assert.Empty(counterparties);
        }

        [Fact]
        public async Task UpdateAsyncShouldCorrectlyUpdateCounterpartyIfCityNameIsTheSame()
        {
            var counterparty = new Counterparty
            {
                Id = 1,
                Name = "Test",
                VAT = "888888888888888888",
                Address = "Address",
                City = new City { Id = 1, Name = "Sofia", },
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            var repo = new EfDeletableEntityRepository<Counterparty>(dbContext);
            var repoCities = new EfDeletableEntityRepository<City>(dbContext);
            await repo.AddAsync(counterparty);
            await repo.SaveChangesAsync();
            var service = new CounterpartiesService(repo, repoCities);
            var input = new EditCounterpartyInputModel
            {
                Id = 1,
                Name = "TestNew",
                VAT = "888888888888888888New",
                Address = "AddressNew",
                CityName = "Sofia",
            };
            await service.UpdateAsync(1, input);

            Assert.Equal("TestNew", counterparty.Name);
            Assert.Equal("888888888888888888New", counterparty.VAT);
            Assert.Equal("AddressNew", counterparty.Address);
            Assert.Equal("Sofia", counterparty.City.Name);
        }

        [Fact]
        public async Task UpdateAsyncShouldCorrectlyUpdateCounterpartyIfCityNameIsNew()
        {
            var counterparty = new Counterparty
            {
                Id = 1,
                Name = "Test",
                VAT = "888888888888888888",
                Address = "Address",
                City = new City { Id = 1, Name = "Sofia", },
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            var repo = new EfDeletableEntityRepository<Counterparty>(dbContext);
            var repoCities = new EfDeletableEntityRepository<City>(dbContext);
            await repo.AddAsync(counterparty);
            await repo.SaveChangesAsync();
            var service = new CounterpartiesService(repo, repoCities);
            var input = new EditCounterpartyInputModel
            {
                Id = 1,
                Name = "TestNew",
                VAT = "888888888888888888New",
                Address = "AddressNew",
                CityName = "Plovdiv",
            };
            await service.UpdateAsync(1, input);

            Assert.Equal("TestNew", counterparty.Name);
            Assert.Equal("888888888888888888New", counterparty.VAT);
            Assert.Equal("AddressNew", counterparty.Address);
            Assert.Equal("Plovdiv", counterparty.City.Name);
        }
    }
}
