namespace AccountingProject.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRolesService
    {
        Task<IEnumerable<T>> GetAllAsync<T>();
    }
}
