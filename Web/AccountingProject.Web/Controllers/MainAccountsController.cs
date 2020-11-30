namespace AccountingProject.Web.Controllers
{
    using System.Threading.Tasks;

    using AccountingProject.Services.Data;
    using AccountingProject.Web.ViewModels.GLAccounts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class MainAccountsController : Controller
    {
        private readonly IMainAccountsService mainAccountsService;

        public MainAccountsController(IMainAccountsService mainAccountsService)
        {
            this.mainAccountsService = mainAccountsService;
        }

        // MainAccounts/Create
        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateMainAccountInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.mainAccountsService.CreateAsync(input);

            // TODO: Redirect to all info page
            // this.RedirectToAction(nameof(actionName));
            return this.Redirect("/");
        }
    }
}
