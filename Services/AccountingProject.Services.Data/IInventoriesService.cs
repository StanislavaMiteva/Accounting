namespace AccountingProject.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AccountingProject.Web.ViewModels.Inventories;

    public interface IInventoriesService
    {
        Task CreateAsync(CreateInventoryInputModel input);

        Task<bool> IsNameAvailableAsync(string name);

        IEnumerable<T> GetAll<T>();

        IEnumerable<T> GetAllByAccount<T>(int accountId);

        Task<T> GetByIdAsync<T>(int id);

        Task UpdateAsync(int id, EditInventoryInputModel input);
    }
}
