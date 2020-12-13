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
        private readonly IDeletableEntityRepository<DocumentType> documentTypesRepository;

        public DocumentTypesService(IDeletableEntityRepository<DocumentType> documentTypesRepository)
        {
            this.documentTypesRepository = documentTypesRepository;
        }

        public async Task CreateAsync(CreateDocumentTypeInputModel input)
        {
            var documentType = new DocumentType
            {
                Name = input.Name,
            };

            await this.documentTypesRepository.AddAsync(documentType);
            await this.documentTypesRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var documentType = await this.documentTypesRepository
                        .All()
                        .FirstOrDefaultAsync(x => x.Id == id);
            this.documentTypesRepository.Delete(documentType);
            await this.documentTypesRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.documentTypesRepository.AllAsNoTracking()
                .To<T>()
                .ToList();
        }

        public async Task<T> GetByIdAsync<T>(int id)
        {
            return await this.documentTypesRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();
        }

        public async Task<bool> IsNameAvailableAsync(string name)
        {
            return !await this.documentTypesRepository
                .AllAsNoTracking()
                .AnyAsync(x => x.Name == name);
        }

        public async Task UpdateAsync(int id, EditDocumentTypeInputModel input)
        {
            var documentType = await this.documentTypesRepository
                .All()
                .FirstOrDefaultAsync(x => x.Id == id);
            documentType.Name = input.Name;
            await this.documentTypesRepository.SaveChangesAsync();
        }
    }
}
