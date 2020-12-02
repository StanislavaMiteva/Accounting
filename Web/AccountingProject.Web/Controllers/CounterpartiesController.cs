namespace AccountingProject.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

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
                this.ModelState.AddModelError("Name", "This name already exists.");
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.counterpartiesService.CreateAsync(input);

            // TODO: Redirect to all info page
            // this.RedirectToAction(nameof(actionName));
            return this.Redirect("/");
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
