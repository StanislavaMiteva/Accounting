namespace AccountingProject.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AccountingProject.Data.Common.Repositories;
    using AccountingProject.Data.Models;
    using AccountingProject.Services.Mapping;
    using AccountingProject.Web.ViewModels.Identity;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly RoleManager<ApplicationRole> roleManager;

        public UsersService(RoleManager<ApplicationRole> roleManager, IDeletableEntityRepository<ApplicationUser> usersRepository)
        {
            this.usersRepository = usersRepository;
            this.roleManager = roleManager;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var user = await this.usersRepository
                        .All()
                        .FirstOrDefaultAsync(x => x.Id == id);
            if (user != null)
            {
                this.usersRepository.Delete(user);
                await this.usersRepository.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            return await this.usersRepository.AllAsNoTracking()
                .OrderBy(x => x.UserName)
                .To<T>()
                .ToListAsync();
        }

        public async Task<IEnumerable<UserViewModel>> GetAllWithDeletedAsync()
        {
            return await this.usersRepository.AllAsNoTrackingWithDeleted()
                .OrderBy(x => x.UserName)
                .Select(x => new UserViewModel
                    {
                        Id = x.Id,
                        UserName = x.UserName,
                        CreatedOn = x.CreatedOn,
                        ModifiedOn = x.ModifiedOn,
                        DeletedOn = x.DeletedOn,
                        Roles = x.Roles
                                .Select(x => new RoleNameIdViewModel
                                {
                                    Id = x.RoleId,
                                    Name = this.roleManager
                                            .FindByIdAsync(x.RoleId)
                                            .GetAwaiter()
                                            .GetResult().Name,
                                })
                                .ToList(),
                    })
                .ToListAsync();
        }
    }
}
