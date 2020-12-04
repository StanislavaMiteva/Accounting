namespace AccountingProject.Web.ViewModels.GLAccounts
{
    using System.Collections.Generic;
    using System.Linq;

    public class TrialBalanceAccountsListViewModel
    {
        public string DateStart { get; set; }

        public string DateEnd { get; set; }

        public IEnumerable<TrialBalanceAccountViewModel> MainAccounts { get; set; }

        public decimal StartDebitBalanceTotal => this.MainAccounts.Sum(x => x.StartDebitBalance);

        public decimal StartCreditBalanceTotal => this.MainAccounts.Sum(x => x.StartCreditBalance);

        public decimal DebitTurnoverTotal => this.MainAccounts.Sum(x => x.DebitTurnoverForPeriod);

        public decimal CreditTurnoverTotal => this.MainAccounts.Sum(x => x.CreditTurnoverForPeriod);

        public decimal EndDebitBalanceTotal => this.MainAccounts.Sum(x => x.EndDebitBalance);

        public decimal EndCreditBalanceTotal => this.MainAccounts.Sum(x => x.EndCreditBalance);
    }
}
