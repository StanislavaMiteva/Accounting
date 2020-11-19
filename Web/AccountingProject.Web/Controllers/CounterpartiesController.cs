namespace AccountingProject.Web.Controllers
{
    using System.Threading.Tasks;

    using AccountingProject.Services.Data;
    using AccountingProject.Web.ViewModels.Counterparties;
    using Microsoft.AspNetCore.Mvc;

    public class CounterpartiesController : Controller
    {
        private readonly ICounterpartiesService counterpartiesService;

        public CounterpartiesController(ICounterpartiesService counterpartiesService)
        {
            this.counterpartiesService = counterpartiesService;
        }

        // Counterparties/Create
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCounterpartyInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.counterpartiesService.CreateAsync(input);

            // TODO: Redirect to all info page
            return this.Redirect("/");
        }
    }
}
