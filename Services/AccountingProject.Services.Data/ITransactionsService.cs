namespace AccountingProject.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AccountingProject.Web.ViewModels.Shared;
    using AccountingProject.Web.ViewModels.Transactions;

    public interface ITransactionsService
    {
        Task CreateAsync(CreateTransactionInputModel input);

        IEnumerable<T> GetAll<T>();

        Task DeleteAsync(string id);

        IEnumerable<T> GetAllTransactionsByMonth<T>(InputYearMonthModel input);

        Task<T> GetByIdAsync<T>(string id);

        Task UpdateAsync(string id, EditTransactionInputModel input);
    }
}
