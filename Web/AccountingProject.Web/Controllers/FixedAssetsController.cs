namespace AccountingProject.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using AccountingProject.Common;
    using AccountingProject.Services.Data;
    using AccountingProject.Web.ViewModels.FixedAssets;
    using AccountingProject.Web.ViewModels.Inventories;
    using AccountingProject.Web.ViewModels.ViewComponents;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AllAccountantsRoleNames)]
    public class FixedAssetsController : Controller
    {
        private readonly IMainAccountsService mainAccountsService;
        private readonly IFixedAssetsService fixedAssetsService;

        public FixedAssetsController(
            IMainAccountsService mainAccountsService,
            IFixedAssetsService fixedAssetsService)
        {
            this.mainAccountsService = mainAccountsService;
            this.fixedAssetsService = fixedAssetsService;
        }

        // FixedAssets/Create
        public IActionResult Create()
        {
            var viewModel = new CreateFixedAssetInputModel
            {
                AcquisitionDate = DateTime.UtcNow,
            };
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateFixedAssetInputModel input)
        {
            if (!await this.fixedAssetsService.IsNameAvailableAsync(input.Name))
            {
                this.ModelState.AddModelError(nameof(input.Name), GlobalConstants.ErrorMessageForExistingName);
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.fixedAssetsService.CreateAsync(input);
            this.TempData["Message"] = $"Fixed asset \"{input.Name}\" has been added successfully.";

            return this.RedirectToAction(nameof(this.Create));
        }

        // FixedAssets/All
        public async Task<IActionResult> All()
        {
            var viewModel = new FixedAssetsListViewModel
            {
                FixedAssets = await this.fixedAssetsService
                    .GetAllAsync<FixedAssetInListViewModel>(),
            };
            return this.View(viewModel);
        }

        // FixedAssets/ById
        public async Task<IActionResult> ByIdAsync(int id)
        {
            var fixedAsset = await this.fixedAssetsService
                .GetByIdAsync<FixedAssetViewModel>(id);
            return this.View(fixedAsset);
        }

        // FixedAssets/Delete
        [HttpPost]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await this.fixedAssetsService.DeleteAsync(id);
            this.TempData["Message"] = $"Fixed asset has been deleted successfully.";
            return this.RedirectToAction(nameof(this.All));
        }
    }
}
