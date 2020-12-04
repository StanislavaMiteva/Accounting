namespace AccountingProject.Web.ViewModels.GLAccounts
{
    public class TrialBalanceAccountViewModel
    {
        public int Code { get; set; }

        public string Name { get; set; }

        public decimal BeginingDebitBalance { get; set; }

        public decimal BeginingCreditBalance { get; set; }

        public decimal DebitTurnoverBeforePeriod { get; set; }

        public decimal CreditTurnoverBeforePeriod { get; set; }

        public decimal StartDebitBalance { get; set; }

        public decimal StartCreditBalance { get; set; }

        public decimal DebitTurnoverForPeriod { get; set; }

        public decimal CreditTurnoverForPeriod { get; set; }

        public decimal EndDebitBalance { get; set; }

        public decimal EndCreditBalance { get; set; }
    }
}
