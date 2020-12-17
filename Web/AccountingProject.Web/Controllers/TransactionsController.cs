namespace AccountingProject.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AccountingProject.Common;
    using AccountingProject.Data.Models;
    using AccountingProject.Services.Data;
    using AccountingProject.Web.ViewModels.Counterparties;
    using AccountingProject.Web.ViewModels.DocumentTypes;
    using AccountingProject.Web.ViewModels.Shared;
    using AccountingProject.Web.ViewModels.Transactions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;

    [Authorize(Roles = GlobalConstants.AllAccountantsRoleNames)]
    public class TransactionsController : Controller
    {
        private readonly ITransactionsService transactionsService;
        private readonly IDocumentTypesService documentTypesService;
        private readonly IMainAccountsService mainAccountsService;
        private readonly IAnalyticalAccountsService analyticalAccountsService;
        private readonly ICounterpartiesService counterpartiesService;
        private readonly IUsersService usersService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMemoryCache memoryCache;

        public TransactionsController(
            ITransactionsService transactionsService,
            IDocumentTypesService documentTypesService,
            IMainAccountsService mainAccountsService,
            IAnalyticalAccountsService analyticalAccountsService,
            ICounterpartiesService counterpartiesService,
            IUsersService usersService,
            UserManager<ApplicationUser> userManager,
            IMemoryCache memoryCache)
        {
            this.transactionsService = transactionsService;
            this.documentTypesService = documentTypesService;
            this.mainAccountsService = mainAccountsService;
            this.analyticalAccountsService = analyticalAccountsService;
            this.counterpartiesService = counterpartiesService;
            this.usersService = usersService;
            this.userManager = userManager;
            this.memoryCache = memoryCache;
        }

        // Transactions/Create
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

                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            input.CreatorId = user.Id;
            await this.transactionsService.CreateAsync(input);
            this.TempData["Message"] = $"Transaction has been added successfully.";
            return this.RedirectToAction(nameof(this.Create));
        }

        // Transactions/Edit
        public async Task<IActionResult> EditAsync(string id)
        {
            var viewModel = await this.transactionsService
                .GetByIdAsync<EditTransactionInputModel>(id);
            var mainAccounts = this.GetMainAccountsFromInCashMemory();
            viewModel.MainAccounts = mainAccounts;
            viewModel.Counterparties = this.counterpartiesService
                                .GetAll<CounterpartyPartViewModel>()
                                .OrderBy(x => x.Name);
            viewModel.Documents = this.documentTypesService
                                 .GetAll<DocumentTypePartViewModel>()
                                 .OrderBy(x => x.Name);
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditAsync(
        [Bind("DocumentDate,DocumentTypeId,DebitMainAccountId," +
            "DebitAnalyticalAccountId,CreditMainAccountId," +
            "CreditAnalyticalAccountId,CounterpartyId,IsPurchase,IsSale," +
            "Description,Folder,ConsecutiveNumber,Amount")]
        string id, EditTransactionInputModel input)
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
                return this.View(input);
            }

            await this.transactionsService.UpdateAsync(id, input);
            this.TempData["Message"] = $"Transaction has been edited successfully.";
            return this.RedirectToAction(nameof(this.ById), new { id });
        }

        // Transactions/AllByDocumentDate
        public IActionResult AllByDocumentDate()
        {
            var viewModel = new TransactionsListViewModel
            {
                Transactions = this.transactionsService
                    .GetAll<TransactionInListViewModel>()
                    .OrderBy(x => x.DocumentDate),
            };
            return this.View(viewModel);
        }

        // Transactions/ById
        public async Task<IActionResult> ById(string id)
        {
            var transaction = await this.transactionsService
                .GetByIdAsync<TransactionViewModel>(id);
            return this.View(transaction);
        }

        // Transactions/Delete
        [HttpPost]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            await this.transactionsService.DeleteAsync(id);
            this.TempData["Message"] = $"Transaction has been deleted successfully.";
            return this.RedirectToAction(nameof(this.AllByDocumentDate));
        }

        // Transactions/ChoosePeriod
        public IActionResult ChoosePeriod()
        {
            return this.View("~/Views/Shared/ChoosePeriod.cshtml");
        }

        [HttpPost]
        public IActionResult ChoosePeriod(InputYearMonthModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View("~/Views/Shared/ChoosePeriod.cshtml", input);
            }

            return this.RedirectToAction("AllForPeriod", "Transactions", input);
        }

        // Transactions/AllForPeriod
        public IActionResult AllForPeriod(InputYearMonthModel input)
        {
            var viewModel = new TransactionsListViewModel
            {
                Transactions = this.transactionsService
                    .GetAllTransactionsByMonth<TransactionInListViewModel>(input),
            };
            return this.View(nameof(this.AllByDocumentDate), viewModel);
        }

        // /Transactions/SearchTransaction
        public async Task<IActionResult> SearchTransaction()
        {
            var currentYear = DateTime.UtcNow.Year;
            var viewModel = new SearchTransactionViewModel
            {
                StartDate = new DateTime(currentYear, 1, 1),
                EndDate = DateTime.UtcNow,
                Counterparties = this.counterpartiesService
                        .GetAll<CounterpartyPartViewModel>()
                        .OrderBy(x => x.Name),
                Documents = this.documentTypesService
                        .GetAll<DocumentTypePartViewModel>(),
                Users = await this.usersService.GetAllWithDeletedAsync(),
            };
            return this.View(viewModel);
        }

        // /Transactions/List?..
        public async Task<IActionResult> List(SearchInputModel input)
        {
            var viewModel = new TransactionsListViewModel
            {
                Transactions = await this.transactionsService
                        .GetByCriteriaAsync<TransactionInListViewModel>(input),
            };
            return this.View(viewModel);
        }

        private IEnumerable<KeyValuePair<string, string>> GetMainAccountsFromInCashMemory()
        {
            if (!this.memoryCache.TryGetValue<IEnumerable<KeyValuePair<string, string>>>("accounts", out var mainAccounts))
            {
                mainAccounts = this.mainAccountsService.GetAllAsKeyValuePairs();

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
