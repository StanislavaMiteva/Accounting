namespace AccountingProject.Web.Controllers
{
    using System.Threading.Tasks;

    using AccountingProject.Services.Data;
    using AccountingProject.Web.ViewModels.DocumentTypes;
    using Microsoft.AspNetCore.Mvc;

    public class DocumentTypesController : Controller
    {
        private readonly IDocumentTypesService documentTypesService;

        public DocumentTypesController(IDocumentTypesService documentTypesService)
        {
            this.documentTypesService = documentTypesService;
        }

        // DocumentTypes/Create
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateDocumentTypeInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.documentTypesService.CreateAsync(input);

            // TODO: Redirect to all info page
            return this.Redirect("/");
        }
    }
}
