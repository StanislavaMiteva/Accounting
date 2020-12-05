namespace AccountingProject.Services.Data
{
    using System.Threading.Tasks;

    using AccountingProject.Web.ViewModels.Inventories;

    public interface IInventoriesService
    {
        Task CreateAsync(CreateInventoryInputModel input);

        Task<bool> IsNameAvailableAsync(string name);
    }
}
