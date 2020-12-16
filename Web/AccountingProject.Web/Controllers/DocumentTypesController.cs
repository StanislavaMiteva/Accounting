namespace AccountingProject.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using AccountingProject.Common;
    using AccountingProject.Services.Data;
    using AccountingProject.Web.ViewModels.DocumentTypes;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AllAccountantsRoleNames)]
    public class DocumentTypesController : Controller
    {
        private readonly IDocumentTypesService documentTypesService;

        public DocumentTypesController(IDocumentTypesService documentTypesService)
        {
            this.documentTypesService = documentTypesService;
        }

        // DocumentTypes/Create
        [Authorize(Roles = GlobalConstants.ChiefAccountantRoleName)]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.ChiefAccountantRoleName)]
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
        [Authorize(Roles = GlobalConstants.ChiefAccountantRoleName)]
        public async Task<IActionResult> EditAsync(int id)
        {
            var viewModel = await this.documentTypesService
                .GetByIdAsync<EditDocumentTypeInputModel>(id);
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.ChiefAccountantRoleName)]
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

        // DocumentTypes/Delete
        [HttpPost]
        [Authorize(Roles = GlobalConstants.ChiefAccountantRoleName)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var documentToDelete = await this.documentTypesService
                .GetByIdAsync<DocumentTypePartViewModel>(id);
            await this.documentTypesService.DeleteAsync(id);
            this.TempData["Message"] = $"Document type \"{documentToDelete.Name}\" has been deleted successfully.";
            return this.RedirectToAction(nameof(this.All));
        }

        // DocumentTypes/All
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
