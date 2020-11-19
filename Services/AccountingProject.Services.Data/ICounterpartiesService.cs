namespace AccountingProject.Services.Data
{
    using System.Threading.Tasks;

    using AccountingProject.Web.ViewModels.Counterparties;

    public interface ICounterpartiesService
    {
        Task CreateAsync(CreateCounterpartyInputModel input);

        // IEnumerable<CounterpartyViewModel> GetAll();

        // Counterparty GetCounterpartyByName(string name);
    }
}
