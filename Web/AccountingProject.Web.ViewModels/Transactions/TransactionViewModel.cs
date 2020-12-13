namespace AccountingProject.Web.ViewModels.Transactions
{
    public class TransactionViewModel : TransactionInListViewModel
    {
        public int DebitGLAccountCode { get; set; }

        public string DebitGLAccountName { get; set; }

        public string DebitAnalyticalAccountName { get; set; }

        public int CreditGLAccountCode { get; set; }

        public string CreditGLAccountName { get; set; }

        public string CreditAnalyticalAccountName { get; set; }

        public string CounterpartyName { get; set; }

        public string CreatorUserName { get; set; }
    }
}
