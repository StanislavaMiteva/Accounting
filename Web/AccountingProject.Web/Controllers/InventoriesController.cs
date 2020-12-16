namespace AccountingProject.Web.Controllers
{
    using System.Threading.Tasks;

    using AccountingProject.Common;
    using AccountingProject.Services.Data;
    using AccountingProject.Web.ViewModels.Inventories;
    using AccountingProject.Web.ViewModels.ViewComponents;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AllAccountantsRoleNames)]
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
        public IActionResult Create()
        {
            var viewModel = new CreateInventoryInputModel { };
            return this.View(viewModel);
        }

        [HttpPost]
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

        // Inventories/Edit
        public async Task<IActionResult> EditAsync(int id)
        {
            var viewModel = await this.inventoriesService
                .GetByIdAsync<EditInventoryInputModel>(id);
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditAsync(
        [Bind("Name, Measure, Quantity, Price, MainAccountId")]
        int id, EditInventoryInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.inventoriesService.UpdateAsync(id, input);
            this.TempData["Message"] = $"Inventory \"{input.Name}\" has been edited successfully.";
            return this.RedirectToAction(nameof(this.All));
        }

        // Inventories/Delete
        [HttpPost]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var inventoryToDelete = await this.inventoriesService
                .GetByIdAsync<InventoryViewModel>(id);
            await this.inventoriesService.DeleteAsync(id);
            this.TempData["Message"] = $"Inventory \"{inventoryToDelete.Name}\" has been deleted successfully.";
            return this.RedirectToAction(nameof(this.All));
        }

        // Inventories/All
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
        public IActionResult ChooseAccount()
        {
            var viewModel = new ListOfMainAccountsViewModel { };
            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult ChooseAccount(ListOfMainAccountsViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            return this.RedirectToAction(nameof(this.AllByAccount), new { mainAccountId = input.MainAccountId });
        }

        // Inventories/AllByAccount
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
