namespace AccountingProject.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using AccountingProject.Common;
    using AccountingProject.Services.Data;
    using AccountingProject.Web.ViewModels.Counterparties;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class CounterpartiesController : Controller
    {
        private readonly ICounterpartiesService counterpartiesService;

        public CounterpartiesController(ICounterpartiesService counterpartiesService)
        {
            this.counterpartiesService = counterpartiesService;
        }

        // Counterparties/Create
        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateCounterpartyInputModel input)
        {
            if (!await this.counterpartiesService.IsNameAvailableAsync(input.Name))
            {
                this.ModelState.AddModelError(nameof(input.Name), GlobalConstants.ErrorMessageForExistingName);
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.counterpartiesService.CreateAsync(input);
            this.TempData["Message"] = $"Counterparty \"{input.Name}\" has been added successfully.";
            return this.RedirectToAction(nameof(this.All));
        }

        // Counterparties/Edit
        [Authorize]
        public async Task<IActionResult> EditAsync(int id)
        {
            var viewModel = await this.counterpartiesService
                .GetByIdAsync<EditCounterpartyInputModel>(id);
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditAsync(
        [Bind("Name, VAT, Address, CityName")]
        int id, EditCounterpartyInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.counterpartiesService.UpdateAsync(id, input);
            this.TempData["Message"] = $"Counterparty \"{input.Name}\" has been edited successfully.";
            return this.RedirectToAction(nameof(this.All));
        }

        // Counterparties/Delete
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await this.counterpartiesService.DeleteAsync(id);
            this.TempData["Message"] = $"Counterparty has been deleted successfully.";
            return this.RedirectToAction(nameof(this.All));
        }

        // Counterparties/All
        [Authorize]
        public IActionResult All()
        {
            var viewModel = new CounterpartiesListViewModel
            {
                Counterparties = this.counterpartiesService
                    .GetAll<CounterpartyViewModel>()
                    .OrderBy(x => x.Name),
            };
            return this.View(viewModel);
        }
    }
}
