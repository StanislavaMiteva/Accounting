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
