namespace AccountingProject.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using AccountingProject.Services.Data;
    using AccountingProject.Web.ViewModels.AnalyticalAccounts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class AnalyticalAccountsController : Controller
    {
        private readonly IAnalyticalAccountsService analyticalAccountsService;
        private readonly IMainAccountsService mainAccountsService;

        public AnalyticalAccountsController(
            IAnalyticalAccountsService analyticalAccountsService,
            IMainAccountsService mainAccountsService)
        {
            this.analyticalAccountsService = analyticalAccountsService;
            this.mainAccountsService = mainAccountsService;
        }

        // AnalyticalAccounts/Create
        [Authorize]
        public IActionResult Create()
        {
            var viewModel = new CreateAnalyticalAccountInputModel
            {
                MainAccounts = this.mainAccountsService.GetAllOnlyIdCodeName(),
            };
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Name,MainAccountId")]CreateAnalyticalAccountInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.MainAccounts = this.mainAccountsService.GetAllOnlyIdCodeName();
                return this.View(input);
            }

            await this.analyticalAccountsService.CreateAsync(input);

            // TODO: Redirect to all info page
            // this.RedirectToAction(nameof(actionName));
            return this.Redirect("/");
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
