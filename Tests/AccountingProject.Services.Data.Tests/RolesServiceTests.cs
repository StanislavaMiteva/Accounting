namespace AccountingProject.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AccountingProject.Data;
    using AccountingProject.Data.Models;
    using AccountingProject.Data.Repositories;
    using AccountingProject.Services.Mapping;
    using AccountingProject.Web.ViewModels.Identity;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class RolesServiceTests
    {
        [Fact]
        public async Task GetAllAsyncShouldReturnCorrectResult()
        {
            var role = new ApplicationRole
            {
                Id = "number",
                Name = "Name",
            };
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            var repo = new EfDeletableEntityRepository<ApplicationRole>(dbContext);
            await repo.AddAsync(role);
            await repo.SaveChangesAsync();
            var service = new RolesService(repo);
            AutoMapperConfig.RegisterMappings(typeof(RoleNameIdViewModel).Assembly);
            var actualResult = (await service.GetAllAsync<RoleNameIdViewModel>()).ToList();
            var expectedResult = new List<RoleNameIdViewModel>
            {
                new RoleNameIdViewModel { Id = "number", Name = "Name", },
            };

            Assert.Single(actualResult);
            Assert.Equal(expectedResult.First().Name, actualResult.First().Name);
            Assert.Equal(expectedResult.First().Id, actualResult.First().Id);
        }
    }
}
