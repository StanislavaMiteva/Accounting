namespace AccountingProject.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AccountingProject.Data.Common.Repositories;
    using AccountingProject.Data.Models;
    using AccountingProject.Services.Mapping;
    using AccountingProject.Web.ViewModels.DocumentTypes;
    using Microsoft.EntityFrameworkCore;

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

        public IEnumerable<T> GetAll<T>()
        {
            return this.documentTypeRepository.AllAsNoTracking()
                .To<T>()
                .ToList();
        }

        public async Task<bool> IsNameAvailableAsync(string name)
        {
            return !await this.documentTypeRepository
                .AllAsNoTracking()
                .AnyAsync(x => x.Name == name);
        }
    }
}
