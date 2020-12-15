namespace AccountingProject.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AccountingProject.Web.ViewModels.Identity;

    public interface IUsersService
    {
        Task<IEnumerable<T>> GetAllAsync<T>();

        Task<IEnumerable<UserViewModel>> GetAllWithDeletedAsync();

        Task<bool> DeleteAsync(string id);
    }
}
