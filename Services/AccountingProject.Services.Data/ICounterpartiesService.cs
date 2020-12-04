namespace AccountingProject.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AccountingProject.Web.ViewModels.Counterparties;

    public interface ICounterpartiesService
    {
        Task CreateAsync(CreateCounterpartyInputModel input);

        IEnumerable<T> GetAll<T>();

        Task<bool> IsNameAvailableAsync(string name);
    }
}
