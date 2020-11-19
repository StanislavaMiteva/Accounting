namespace AccountingProject.Services.Data
{
    using System.Threading.Tasks;

    using AccountingProject.Data.Common.Repositories;
    using AccountingProject.Data.Models;
    using AccountingProject.Web.ViewModels.DocumentTypes;

    public class DocumentTypesService : IDocumentTypesService
    {
        private readonly IDeletableEntityRepository<DocumentType> documentTypeRepository;

        public DocumentTypesService(IDeletableEntityRepository<DocumentType> documentTypeRepository)
        {
            this.documentTypeRepository = documentTypeRepository;
        }

        public async Task CreateAsync(CreateDocumentTypeInputModel input)
        {
            var documentType = new DocumentType
            {
                Name = input.Name,
            };

            await this.documentTypeRepository.AddAsync(documentType);
            await this.documentTypeRepository.SaveChangesAsync();
        }
    }
}
