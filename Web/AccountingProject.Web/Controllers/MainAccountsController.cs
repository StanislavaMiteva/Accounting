namespace AccountingProject.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using AccountingProject.Common;
    using AccountingProject.Services.Data;
    using AccountingProject.Web.ViewModels.GLAccounts;
    using AccountingProject.Web.ViewModels.Shared;
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
            this.TempData["Message"] = $"Account \"{input.Code} - {input.Name}\" has been added successfully.";
            return this.RedirectToAction(nameof(this.All));
        }

        // MainAccounts/All
        [Authorize]
        public IActionResult All()
        {
            var viewModel = new MainAccountsListViewModel
            {
                MainAccounts = this.mainAccountsService
                    .GetAll<MainAccountViewModel>(),
            };
            return this.View(viewModel);
        }

        // MainAccounts/SetBalance
        [Authorize]
        public IActionResult SetBalance()
        {
            var viewModel = new AddAccountBalanceInputModel { };
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SetBalance([Bind("MainAccountId" +
            ",AnalyticalAccountId,DebitBalance,CreditBalance")]
        AddAccountBalanceInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.AnalyticalAccountName = this.analyticalAccountsService
                                            .GetNameById(input.AnalyticalAccountId);
                return this.View(input);
            }

            await this.mainAccountsService.InputBalanceAsync(input);

            return this.RedirectToAction(nameof(this.All));
        }

        // MainAccounts/ChoosePeriod
        [Authorize]
        public IActionResult ChoosePeriod()
        {
            return this.View("~/Views/Shared/ChoosePeriod.cshtml");
        }

        [HttpPost]
        [Authorize]
        public IActionResult ChoosePeriod(InputYearMonthModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View("~/Views/Shared/ChoosePeriod.cshtml", input);
            }

            return this.RedirectToAction("TrialBalance", "MainAccounts", input);
        }

        // MainAccounts/TrialBalance
        [Authorize]
        public IActionResult TrialBalance(InputYearMonthModel input)
        {
            DateTime startDate = new DateTime(input.Year, input.MonthStart, 01);
            int daysOfMonth = DateTime.DaysInMonth(input.Year, input.MonthEnd);
            DateTime endDate = new DateTime(input.Year, input.MonthEnd, daysOfMonth);
            var viewModel = new TrialBalanceAccountsListViewModel
            {
                MainAccounts = this.mainAccountsService
                     .AllWithTurnoverForPeriod(startDate, endDate),
                DateStart = startDate.ToString("dd.MM.yyyy"),
                DateEnd = endDate.ToString("dd.MM.yyyy"),
            };
            return this.View(viewModel);
        }
    }
}
