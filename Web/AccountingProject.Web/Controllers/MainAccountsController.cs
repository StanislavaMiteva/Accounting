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
        private readonly IAnalyticalAccountsService analyticalAccountsService;

        public MainAccountsController(
            IMainAccountsService mainAccountsService,
            IAnalyticalAccountsService analyticalAccountsService)
        {
            this.mainAccountsService = mainAccountsService;
            this.analyticalAccountsService = analyticalAccountsService;
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

        // MainAccounts/AddBalances
        [Authorize]
        public IActionResult AddBalances()
        {
            var viewModel = new AddAccountBalanceInputModel
            {
                MainAccounts = this.mainAccountsService
                                    .GetAll<MainAccountPartViewModel>()
                                    .OrderBy(x => x.Code),
            };
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddBalances([Bind("DebitMainAccountId" +
            ",AnalyticalAccountId,DebitBalance,CreditBalance")]
        AddAccountBalanceInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.MainAccounts = this.mainAccountsService
                                            .GetAll<MainAccountPartViewModel>()
                                            .OrderBy(x => x.Code);
                input.AnalyticalAccountName = this.analyticalAccountsService
                                            .GetNameById(input.AnalyticalAccountId);
                return this.View(input);
            }

            await this.mainAccountsService.InputBalanceAsync(input);

            // TODO: Redirect to all info page
            // this.RedirectToAction(nameof(actionName));
            return this.Redirect("/");
        }
    }
}
