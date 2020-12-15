namespace AccountingProject.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using AccountingProject.Data.Models;
    using AccountingProject.Services.Data;
    using AccountingProject.Web.ViewModels.Administration.Dashboard;
    using AccountingProject.Web.ViewModels.Identity;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class DashboardController : AdministrationController
    {
        private readonly ISettingsService settingsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly IUsersService usersService;
        private readonly IRolesService rolesService;

        public DashboardController(
            ISettingsService settingsService,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            IUsersService usersService,
            IRolesService rolesService)
        {
            this.settingsService = settingsService;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.usersService = usersService;
            this.rolesService = rolesService;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel { SettingsCount = this.settingsService.GetCount(), };
            return this.View(viewModel);
        }

        // Dashboard/AddUserToRole
        public async Task<IActionResult> AddUserToRole()
        {
            var viewModel = new AddRoleToUserInputModel
            {
                Users = await this.usersService.GetAllAsync<UserNameIdViewModel>(),
                Roles = await this.rolesService.GetAllAsync<RoleNameIdViewModel>(),
            };
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddUserToRole([Bind("UserId, RoleId")]
            AddRoleToUserInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.FindByIdAsync(input.UserId);
            var role = await this.roleManager.FindByIdAsync(input.RoleId);
            await this.userManager.AddToRoleAsync(user, role.Name);
            this.TempData["Message"] = $"User \"{user.UserName}\" has been added to role \"{role.Name}\" successfully.";

            // return this.RedirectToAction(nameof(this.All));
            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
