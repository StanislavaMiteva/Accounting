namespace AccountingProject.Web.ViewModels.AnalyticalAccounts
{
    using AccountingProject.Data.Models;
    using AccountingProject.Services.Mapping;

    public class AnalyticalAccountViewModel : IMapFrom<AnalyticalAccount>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int GLAccountCode { get; set; }

        public string GLAccountName { get; set; }

        public decimal DebitBalance { get; set; }

        public decimal CreditBalance { get; set; }
    }
}
