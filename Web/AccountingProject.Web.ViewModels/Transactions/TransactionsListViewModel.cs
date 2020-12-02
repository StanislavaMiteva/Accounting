namespace AccountingProject.Web.ViewModels.Transactions
{
    using System.Collections.Generic;

    public class TransactionsListViewModel
    {
        public IEnumerable<TransactionViewModel> Transactions { get; set; }
    }
}
