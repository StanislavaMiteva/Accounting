namespace AccountingProject.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using AccountingProject.Data.Models;
    using AccountingProject.Services.Data;
    using AccountingProject.Web.ViewModels.Transactions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class TransactionsController : Controller
    {
        private readonly ITransactionsService transactionsService;
        private readonly IDocumentTypesService documentTypesService;
        private readonly IMainAccountsService mainAccountsService;
        private readonly IAnalyticalAccountsService analyticalAccountsService;
        private readonly ICounterpartiesService counterpartiesService;
        private readonly UserManager<ApplicationUser> userManager;

        public TransactionsController(
            ITransactionsService transactionsService,
            IDocumentTypesService documentTypesService,
            IMainAccountsService mainAccountsService,
            IAnalyticalAccountsService analyticalAccountsService,
            ICounterpartiesService counterpartiesService,
            UserManager<ApplicationUser> userManager)
        {
            this.transactionsService = transactionsService;
            this.documentTypesService = documentTypesService;
            this.mainAccountsService = mainAccountsService;
            this.analyticalAccountsService = analyticalAccountsService;
            this.counterpartiesService = counterpartiesService;
            this.userManager = userManager;
        }

        // Transactions/Create
        [Authorize]
        public IActionResult Create()
        {
            var viewModel = new CreateTransactionInputModel
            {
                MainAccounts = this.mainAccountsService.GetAllOnlyIdCodeName(),

                Counterparties = this.counterpartiesService.GetAllOnlyIdName(),
                Documents = this.documentTypesService.GetAllOnlyIdName(),
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
                input.MainAccounts = this.mainAccountsService.GetAllOnlyIdCodeName();

                input.Counterparties = this.counterpartiesService.GetAllOnlyIdName();
                input.Documents = this.documentTypesService.GetAllOnlyIdName();
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
    }
}
