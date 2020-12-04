namespace AccountingProject.Web.ViewModels.GLAccounts
{
    using System.Collections.Generic;
    using System.Linq;

    public class MainAccountsListViewModel
    {
        public IEnumerable<MainAccountViewModel> MainAccounts { get; set; }

        public decimal DebitBalanceTotal => this.MainAccounts.Sum(x => x.DebitBalance);

        public decimal CreditBalanceTotal => this.MainAccounts.Sum(x => x.CreditBalance);
    }
}
