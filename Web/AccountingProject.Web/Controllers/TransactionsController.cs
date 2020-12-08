namespace AccountingProject.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AccountingProject.Data.Models;
    using AccountingProject.Services.Data;
    using AccountingProject.Web.ViewModels.Counterparties;
    using AccountingProject.Web.ViewModels.DocumentTypes;
    using AccountingProject.Web.ViewModels.GLAccounts;
    using AccountingProject.Web.ViewModels.Shared;
    using AccountingProject.Web.ViewModels.Transactions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;

    public class TransactionsController : Controller
    {
        private readonly ITransactionsService transactionsService;
        private readonly IDocumentTypesService documentTypesService;
        private readonly IMainAccountsService mainAccountsService;
        private readonly IAnalyticalAccountsService analyticalAccountsService;
        private readonly ICounterpartiesService counterpartiesService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMemoryCache memoryCache;

        public TransactionsController(
            ITransactionsService transactionsService,
            IDocumentTypesService documentTypesService,
            IMainAccountsService mainAccountsService,
            IAnalyticalAccountsService analyticalAccountsService,
            ICounterpartiesService counterpartiesService,
            UserManager<ApplicationUser> userManager,
            IMemoryCache memoryCache)
        {
            this.transactionsService = transactionsService;
            this.documentTypesService = documentTypesService;
            this.mainAccountsService = mainAccountsService;
            this.analyticalAccountsService = analyticalAccountsService;
            this.counterpartiesService = counterpartiesService;
            this.userManager = userManager;
            this.memoryCache = memoryCache;
        }

        // Transactions/Create
        [Authorize]
        public IActionResult Create()
        {
            var mainAccounts = this.GetMainAccountsFromInCashMemory();

            var viewModel = new CreateTransactionInputModel
            {
                MainAccounts = mainAccounts,
                Counterparties = this.counterpartiesService
                                    .GetAll<CounterpartyPartViewModel>()
                                    .OrderBy(x => x.Name),
                Documents = this.documentTypesService
                                    .GetAll<DocumentTypePartViewModel>()
                                    .OrderBy(x => x.Name),
                DocumentDate = DateTime.UtcNow,
            };
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([Bind("DocumentDate," +
            "DocumentTypeId,DebitMainAccountId,DebitAnalyticalAccountId," +
            "CreditMainAccountId,CreditAnalyticalAccountId,CounterpartyId," +
            "IsPurchase,IsSale,Description,Folder,ConsecutiveNumber,Amount")]
        CreateTransactionInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                var mainAccounts = this.GetMainAccountsFromInCashMemory();

                input.MainAccounts = mainAccounts;
                input.Counterparties = this.counterpartiesService
                                            .GetAll<CounterpartyPartViewModel>()
                                            .OrderBy(x => x.Name);
                input.Documents = this.documentTypesService
                                            .GetAll<DocumentTypePartViewModel>()
                                            .OrderBy(x => x.Name);
                input.DebitAnalyticalAccountName = this.analyticalAccountsService
                                            .GetNameById(input.DebitAnalyticalAccountId);
                input.CreditAnalyticalAccountName = this.analyticalAccountsService
                                            .GetNameById(input.CreditAnalyticalAccountId);
                input.DocumentDate = DateTime.UtcNow;
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            input.CreatorId = user.Id;
            await this.transactionsService.CreateAsync(input);

            // TODO: Redirect to all info page
            // this.RedirectToAction(nameof(actionName));
            return this.Redirect("/");
        }

        // Transactions/AllByDocumentDate
        [Authorize]
        public IActionResult AllByDocumentDate()
        {
            var viewModel = new TransactionsListViewModel
            {
                Transactions = this.transactionsService
                    .GetAll<TransactionViewModel>()
                    .OrderBy(x => x.DocumentDate),
            };
            return this.View(viewModel);
        }

        // Transactions/Delete
        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            await this.transactionsService.DeleteAsync(id);

            return this.RedirectToAction(nameof(this.AllByDocumentDate));
        }

        // Transactions/ForPeriod
        [Authorize]
        public IActionResult ForPeriod()
        {
            return this.View("~/Views/Shared/ChoosePeriod.cshtml");
        }

        [HttpPost]
        [Authorize]
        public IActionResult ForPeriod(InputYearMonthModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View("~/Views/Shared/ChoosePeriod.cshtml", input);
            }

            var viewModel = new TransactionsListViewModel
            {
                Transactions = this.transactionsService
                    .GetAllTransactionsByMonth<TransactionViewModel>(input),
            };
            return this.View(nameof(this.AllByDocumentDate), viewModel);
        }

        private IEnumerable<MainAccountPartViewModel> GetMainAccountsFromInCashMemory()
        {
            if (!this.memoryCache.TryGetValue<IEnumerable<MainAccountPartViewModel>>("accounts", out var mainAccounts))
            {
                mainAccounts = this.mainAccountsService
                                    .GetAll<MainAccountPartViewModel>();
                var cashEntryOptions = new MemoryCacheEntryOptions
                {
                    SlidingExpiration = new TimeSpan(0, 0, 20),
                };
                this.memoryCache.Set("accounts", mainAccounts, cashEntryOptions);
            }

            return mainAccounts;
        }
    }
}
