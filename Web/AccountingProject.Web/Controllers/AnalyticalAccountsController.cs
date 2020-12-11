namespace AccountingProject.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using AccountingProject.Common;
    using AccountingProject.Services.Data;
    using AccountingProject.Web.ViewModels.AnalyticalAccounts;
    using AccountingProject.Web.ViewModels.GLAccounts;
    using AccountingProject.Web.ViewModels.ViewComponents;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class AnalyticalAccountsController : Controller
    {
        private readonly IAnalyticalAccountsService analyticalAccountsService;
        private readonly IMainAccountsService mainAccountsService;

        public AnalyticalAccountsController(IAnalyticalAccountsService analyticalAccountsService, IMainAccountsService mainAccountsService)
        {
            this.analyticalAccountsService = analyticalAccountsService;
            this.mainAccountsService = mainAccountsService;
        }

        // AnalyticalAccounts/Create
        [Authorize]
        public IActionResult Create()
        {
            var viewModel = new CreateAnalyticalAccountInputModel { };
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Name,MainAccountId")]
        CreateAnalyticalAccountInputModel input)
        {
            if (!await this.analyticalAccountsService.IsNameAvailableAsync(input.Name, input.MainAccountId))
            {
                this.ModelState.AddModelError(nameof(input.Name), GlobalConstants.ErrorMessageForExistingName);
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.analyticalAccountsService.CreateAsync(input);
            this.TempData["Message"] = $"Analytical account \"{input.Name}\" has been added successfully.";
            return this.RedirectToAction(nameof(this.All));
        }

        // AnalyticalAccounts/Edit
        [Authorize]
        public async Task<IActionResult> EditAsync(int id)
        {
            var viewModel = await this.analyticalAccountsService
                .GetByIdAsync<EditAnalyticalAccountInputModel>(id);
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(
        [Bind("Name")]
        int id, EditAnalyticalAccountInputModel input)
        {
            if (!await this.analyticalAccountsService.IsNameAvailableAsync(input.Name, input.MainAccountId))
            {
                this.ModelState.AddModelError(nameof(input.Name), GlobalConstants.ErrorMessageForExistingName);
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.analyticalAccountsService.UpdateAsync(id, input);
            this.TempData["Message"] = $"Analytical account \"{input.Name}\" has been edited successfully.";
            return this.RedirectToAction(nameof(this.All));
        }

        // AnalyticalAccounts/All
        [Authorize]
        public IActionResult All()
        {
            var viewModel = new AnalyticalAccountsListViewModel
            {
                AnalyticalAccounts = this.analyticalAccountsService
                    .GetAll<AnalyticalAccountViewModel>()
                    .OrderBy(x => x.GLAccountCode)
                    .ThenBy(x => x.Name),
            };
            return this.View(viewModel);
        }
    }
}
