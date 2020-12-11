namespace AccountingProject.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using AccountingProject.Common;
    using AccountingProject.Services.Data;
    using AccountingProject.Web.ViewModels.DocumentTypes;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class DocumentTypesController : Controller
    {
        private readonly IDocumentTypesService documentTypesService;

        public DocumentTypesController(IDocumentTypesService documentTypesService)
        {
            this.documentTypesService = documentTypesService;
        }

        // DocumentTypes/Create
        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateDocumentTypeInputModel input)
        {
            if (!await this.documentTypesService.IsNameAvailableAsync(input.Name))
            {
                this.ModelState.AddModelError(nameof(input.Name), GlobalConstants.ErrorMessageForExistingName);
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.documentTypesService.CreateAsync(input);
            this.TempData["Message"] = $"Document type \"{input.Name}\" has been added successfully.";
            return this.RedirectToAction(nameof(this.All));
        }

        // DocumentTypes/Edit
        [Authorize]
        public async Task<IActionResult> EditAsync(int id)
        {
            var viewModel = await this.documentTypesService
                .GetByIdAsync<EditDocumentTypeInputModel>(id);
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(
        [Bind("Name")]
        int id, EditDocumentTypeInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.documentTypesService.UpdateAsync(id, input);
            this.TempData["Message"] = $"Document type \"{input.Name}\" has been edited successfully.";
            return this.RedirectToAction(nameof(this.All));
        }

        // DocumentTypes/All
        [Authorize]
        public IActionResult All()
        {
            var viewModel = new DocumentTypesListViewModel
            {
                DocumentTypes = this.documentTypesService
                    .GetAll<DocumentTypeViewModel>()
                    .OrderBy(x => x.Name),
            };
            return this.View(viewModel);
        }
    }
}
