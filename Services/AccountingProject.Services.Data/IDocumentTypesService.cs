namespace AccountingProject.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AccountingProject.Web.ViewModels.DocumentTypes;

    public interface IDocumentTypesService
    {
        Task CreateAsync(CreateDocumentTypeInputModel input);

        IEnumerable<T> GetAll<T>();

        Task<bool> IsNameAvailableAsync(string name);

        Task<T> GetByIdAsync<T>(int id);

        Task UpdateAsync(int id, EditDocumentTypeInputModel input);

        Task DeleteAsync(int id);
    }
}
