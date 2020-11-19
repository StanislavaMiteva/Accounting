namespace AccountingProject.Web.Controllers
{
    using System.Threading.Tasks;

    using AccountingProject.Services.Data;
    using AccountingProject.Web.ViewModels.AnalyticalAccounts;
    using Microsoft.AspNetCore.Mvc;

    public class AnalyticalAccountsController : Controller
    {
        private readonly IAnalyticalAccountsService analyticalAccountsService;
        private readonly IGLAccountsService gLAccountsService;

        public AnalyticalAccountsController(IAnalyticalAccountsService analyticalAccountsService,
            IGLAccountsService gLAccountsService)
        {
            this.analyticalAccountsService = analyticalAccountsService;
            this.gLAccountsService = gLAccountsService;
        }

        // AnalyticalAccounts/Create
        public IActionResult Create()
        {
            var viewModel = new CreateAnalyticalAccountInputModel();
            viewModel.MainAccounts = this.gLAccountsService.GetAllOnlyIdCodeName();
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name,MainAccountId")]CreateAnalyticalAccountInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.MainAccounts = this.gLAccountsService.GetAllOnlyIdCodeName();
                return this.View(input);
            }

            await this.analyticalAccountsService.CreateAsync(input);

            // TODO: Redirect to all info page
            return this.Redirect("/");
        }
    }
}
