namespace AccountingProject.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AccountingProject.Web.ViewModels.FixedAssets;

    public interface IFixedAssetsService
    {
        Task CreateAsync(CreateFixedAssetInputModel input);

        Task<bool> IsNameAvailableAsync(string name);

        Task<IEnumerable<T>> GetAllAsync<T>();

        Task<T> GetByIdAsync<T>(int id);

        Task DeleteAsync(int id);

        Task UpdateAsync(int id, EditFixedAssetInputModel input);
    }
}
