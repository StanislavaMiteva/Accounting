namespace AccountingProject.Web.ViewModels.Transactions
{
    using System.Collections.Generic;
    using System.Linq;

    public class TransactionsListViewModel
    {
        public IEnumerable<TransactionViewModel> Transactions { get; set; }

        public decimal TotalAmount => this.Transactions.Sum(x => x.Amount);
    }
}
