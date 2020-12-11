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

        public async Task<T> GetByIdAsync<T>(int id)
        {
            return await this.documentTypeRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();
        }

        public async Task<bool> IsNameAvailableAsync(string name)
        {
            return !await this.documentTypeRepository
                .AllAsNoTracking()
                .AnyAsync(x => x.Name == name);
        }

        public async Task UpdateAsync(int id, EditDocumentTypeInputModel input)
        {
            var documentType = await this.documentTypeRepository
                .All()
                .FirstOrDefaultAsync(x => x.Id == id);
            documentType.Name = input.Name;
            await this.documentTypeRepository.SaveChangesAsync();
        }
    }
}
