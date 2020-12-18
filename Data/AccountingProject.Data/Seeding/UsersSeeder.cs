namespace AccountingProject.Data.Seeding
{
    using System;
    using System.Threading.Tasks;

    using AccountingProject.Common;
    using AccountingProject.Data.Models;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public class UsersSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var admin = await userManager.FindByNameAsync("Administrator@abv.bg");
            if (admin == null)
            {
                var userAdmin = new ApplicationUser
                {
                    UserName = "Administrator@abv.bg",
                    NormalizedUserName = "ADMINISTRATOR@ABV.BG",
                    Email = "Administrator@abv.bg",
                    NormalizedEmail = "ADMINISTRATOR@ABV.BG",
                };

                var result = await userManager.CreateAsync(userAdmin, "Administrator@abv.bg");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(userAdmin, GlobalConstants.AdministratorRoleName);
                }
            }
        }
    }
}
