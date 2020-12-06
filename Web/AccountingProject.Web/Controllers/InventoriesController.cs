namespace AccountingProject.Web.Controllers
{
    using System.Threading.Tasks;

    using AccountingProject.Common;
    using AccountingProject.Services.Data;
    using AccountingProject.Web.ViewModels.GLAccounts;
    using AccountingProject.Web.ViewModels.Inventories;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class InventoriesController : Controller
    {
        private readonly IMainAccountsService mainAccountsService;
        private readonly IInventoriesService inventoriesService;

        public InventoriesController(IMainAccountsService mainAccountsService, IInventoriesService inventoriesService)
        {
            this.mainAccountsService = mainAccountsService;
            this.inventoriesService = inventoriesService;
        }

        // Inventories/Create
        [Authorize]
        public IActionResult Create()
        {
            var viewModel = new CreateInventoryInputModel
            {
                MainAccounts = this.mainAccountsService
                                        .GetInventoryAccounts<MainAccountPartViewModel>(),
            };
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Name,Measure," +
            "Quantity,Price,MainAccountId")]
            CreateInventoryInputModel input)
        {
            if (!await this.inventoriesService.IsNameAvailableAsync(input.Name))
            {
                this.ModelState.AddModelError(nameof(input.Name), GlobalConstants.ErrorMessageForExistingName);
            }

            if (!this.ModelState.IsValid)
            {
                input.MainAccounts = this.mainAccountsService
                                            .GetInventoryAccounts<MainAccountPartViewModel>();
                return this.View(input);
            }

            await this.inventoriesService.CreateAsync(input);

            // TODO: Redirect to all info page
            // this.RedirectToAction(nameof(actionName));
            return this.Redirect("/");
        }

        // Inventories/All
        [Authorize]
        public IActionResult All()
        {
            var viewModel = new InventoriesListViewModel
            {
                Inventories = this.inventoriesService
                    .GetAll<InventoryViewModel>(),
            };
            return this.View(viewModel);
        }

        // Inventories/AllByAccount
        [Authorize]
        public IActionResult AllByAccount()
        {
            var viewModel = new ListAcountsViewModel
            {
                MainAccounts = this.mainAccountsService
                       .GetInventoryAccounts<MainAccountPartViewModel>(),
            };
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public IActionResult AllByAccount(int mainAccountId)
        {
            if (!this.ModelState.IsValid)
            {
                var model = new ListAcountsViewModel
                {
                    MainAccounts = this.mainAccountsService
                       .GetInventoryAccounts<MainAccountPartViewModel>(),
                };
                return this.View(model);
            }

            var viewModel = new InventoriesListViewModel
            {
                Inventories = this.inventoriesService
                    .GetAllByAccount<InventoryViewModel>(mainAccountId),
            };
            return this.View(nameof(this.All), viewModel);
        }
    }
}
