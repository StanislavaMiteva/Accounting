namespace AccountingProject.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using AccountingProject.Common;
    using AccountingProject.Services.Data;
    using AccountingProject.Web.ViewModels.AnalyticalAccounts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class AnalyticalAccountsController : Controller
    {
        private readonly IAnalyticalAccountsService analyticalAccountsService;

        public AnalyticalAccountsController(IAnalyticalAccountsService analyticalAccountsService)
        {
            this.analyticalAccountsService = analyticalAccountsService;
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
            if (!await this.analyticalAccountsService.IsNameAvailableAsync(input.Name))
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
