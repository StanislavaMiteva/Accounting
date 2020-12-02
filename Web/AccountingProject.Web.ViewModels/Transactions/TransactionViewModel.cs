namespace AccountingProject.Web.ViewModels.Transactions
{
    using System;

    using AccountingProject.Data.Models;
    using AccountingProject.Services.Mapping;

    public class TransactionViewModel : IMapFrom<Transaction>
    {
        public string Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedOnAsString => this.CreatedOn.ToString("dd/MM/yyyy");

        public DateTime DocumentDate { get; set; }

        public string DocumentDateAsString => this.DocumentDate.ToString("dd/MM/yyyy");

        public string DocumentTypeName { get; set; }

        public string Description { get; set; }

        public int DebitGLAccountCode { get; set; }

        public string DebitGLAccountName { get; set; }

        public string DebitAnalyticalAccountName { get; set; }

        public int CreditGLAccountCode { get; set; }

        public string CreditGLAccountName { get; set; }

        public string CreditAnalyticalAccountName { get; set; }

        public decimal Amount { get; set; }

        public string CounterpartyName { get; set; }

        public string CreatorUserName { get; set; }

        public string Folder { get; set; }

        public string ConsecutiveNumber { get; set; }
    }
}
