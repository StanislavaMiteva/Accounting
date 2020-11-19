namespace AccountingProject.Services.Data
{
    using System.Threading.Tasks;

    using AccountingProject.Web.ViewModels.DocumentTypes;

    public interface IDocumentTypesService
    {
        Task CreateAsync(CreateDocumentTypeInputModel input);

        // IEnumerable<DocumentTypeViewModel> GetAllDocumentTypes();
    }
}
