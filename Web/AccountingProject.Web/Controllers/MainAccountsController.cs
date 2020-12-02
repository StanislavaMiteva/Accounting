namespace AccountingProject.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using AccountingProject.Common;
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
            if (!await this.mainAccountsService.IsCodeAvailableAsync(input.Code))
            {
                this.ModelState.AddModelError(nameof(input.Code), GlobalConstants.ErrorMessageForExistingCode);
            }

            if (!await this.mainAccountsService.IsNameAvailableAsync(input.Name))
            {
                this.ModelState.AddModelError(nameof(input.Name), GlobalConstants.ErrorMessageForExistingName);
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.mainAccountsService.CreateAsync(input);

            // TODO: Redirect to all info page
            // this.RedirectToAction(nameof(actionName));
            return this.Redirect("/");
        }

        // MainAccounts/All
        [Authorize]
        public IActionResult All()
        {
            var viewModel = new MainAccountsListViewModel
            {
                MainAccounts = this.mainAccountsService
                    .GetAll<MainAccountViewModel>()
                    .OrderBy(x => x.Code),
            };
            return this.View(viewModel);
        }
    }
}
