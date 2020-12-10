namespace AccountingProject.Web.Controllers
{
    using System.Threading.Tasks;

    using AccountingProject.Common;
    using AccountingProject.Services.Data;
    using AccountingProject.Web.ViewModels.Inventories;
    using AccountingProject.Web.ViewModels.ViewComponents;
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
            var viewModel = new CreateInventoryInputModel { };
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
                return this.View(input);
            }

            await this.inventoriesService.CreateAsync(input);
            this.TempData["Message"] = $"Inventory \"{input.Name}\" has been added successfully.";
            return this.RedirectToAction(nameof(this.All));
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

        // Inventories/ChooseAccount
        [Authorize]
        public IActionResult ChooseAccount()
        {
            var viewModel = new ListOfMainAccountsViewModel { };
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public IActionResult ChooseAccount(ListOfMainAccountsViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            return this.RedirectToAction("AllByAccount", "Inventories", new { mainAccountId = input.MainAccountId });
        }

        // Inventories/AllByAccount
        [Authorize]
        public IActionResult AllByAccount(int mainAccountId)
        {
            var viewModel = new InventoriesListViewModel
            {
                Inventories = this.inventoriesService
                    .GetAllByAccount<InventoryViewModel>(mainAccountId),
            };
            return this.View(nameof(this.All), viewModel);
        }
    }
}
