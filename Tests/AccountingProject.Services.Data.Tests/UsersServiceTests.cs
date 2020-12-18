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
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class UsersServiceTests
    {
        private readonly RoleManager<ApplicationRole> roleManager;

        [Fact]
        public async Task GetAllAsyncShouldReturnCorrectResult()
        {
            var user = new ApplicationUser
            {
                Id = "userId",
                UserName = "Name",
            };
            var userDeleted = new ApplicationUser
            {
                Id = "userIdDel",
                UserName = "NameDel",
                IsDeleted = true,
            };
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            var usersRepo = new EfDeletableEntityRepository<ApplicationUser>(dbContext);

            await usersRepo.AddAsync(user);
            await usersRepo.AddAsync(userDeleted);
            await usersRepo.SaveChangesAsync();
            var service = new UsersService(this.roleManager, usersRepo);
            AutoMapperConfig.RegisterMappings(typeof(UserNameIdViewModel).Assembly);
            var actualResult = (await service.GetAllAsync<UserNameIdViewModel>()).ToList();
            var expectedResult = new List<UserNameIdViewModel>
            {
                new UserNameIdViewModel { Id = "userId", UserName = "Name", },
            };

            Assert.Single(actualResult);
            Assert.Equal(expectedResult.First().UserName, actualResult.First().UserName);
            Assert.Equal(expectedResult.First().Id, actualResult.First().Id);
        }

        [Fact]
        public async Task GetAllWithDeletedAsyncShouldReturnCorrectResult()
        {
            var user = new ApplicationUser
            {
                Id = "userId",
                UserName = "Name",
                IsDeleted = false,
            };
            var userDeleted = new ApplicationUser
            {
                Id = "userIdDel",
                UserName = "NameDel",
                IsDeleted = true,
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            var usersRepo = new EfDeletableEntityRepository<ApplicationUser>(dbContext);

            await usersRepo.AddAsync(userDeleted);
            await usersRepo.AddAsync(user);
            await usersRepo.SaveChangesAsync();
            var service = new UsersService(this.roleManager, usersRepo);
            AutoMapperConfig.RegisterMappings(typeof(UserNameIdViewModel).Assembly);
            var actualResult = (await service.GetAllWithDeletedAsync()).ToList();

            Assert.Equal(2, actualResult.Count);
        }

        [Fact]
        public async Task DeleteAsyncShouldReturnTrueIfUserExists()
        {
            var user = new ApplicationUser
            {
                Id = "userId",
                UserName = "Name",
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            var usersRepo = new EfDeletableEntityRepository<ApplicationUser>(dbContext);

            await usersRepo.AddAsync(user);
            await usersRepo.SaveChangesAsync();
            var service = new UsersService(this.roleManager, usersRepo);
            AutoMapperConfig.RegisterMappings(typeof(UserNameIdViewModel).Assembly);
            var actualResult = await service.DeleteAsync(user.Id);

            Assert.True(actualResult);
        }

        [Fact]
        public async Task DeleteAsyncShouldReturnFalseIfUserDoesNotExist()
        {
            var user = new ApplicationUser
            {
                Id = "userId",
                UserName = "Name",
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            var usersRepo = new EfDeletableEntityRepository<ApplicationUser>(dbContext);

            await usersRepo.AddAsync(user);
            await usersRepo.SaveChangesAsync();
            var service = new UsersService(this.roleManager, usersRepo);
            AutoMapperConfig.RegisterMappings(typeof(UserNameIdViewModel).Assembly);
            var actualResult = await service.DeleteAsync("anotherId");

            Assert.False(actualResult);
        }
    }
}
