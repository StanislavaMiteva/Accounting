namespace AccountingProject.Services.Data
{
    using System.Threading.Tasks;

    using AccountingProject.Web.ViewModels.Transactions;

    public interface ITransactionsService
    {
        Task CreateAsync(CreateTransactionInputModel input);

        // IEnumerable<TransactionViewModel> GetAllTransactions();

        // void Delete(string id);

        // IEnumerable<TransactionViewModel> GetAllTransactionsByMonth(InputYearMonthModel input);
    }
}
