namespace AccountingProject.Web.ViewModels.Transactions
{
    using System;

    using AccountingProject.Data.Models;
    using AccountingProject.Services.Mapping;

    public class TransactionInListViewModel : IMapFrom<Transaction>
    {
        public string Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedOnAsString => this.CreatedOn.ToString("dd/MM/yyyy");

        public DateTime DocumentDate { get; set; }

        public string DocumentDateAsString => this.DocumentDate.ToString("dd/MM/yyyy");

        public string DocumentTypeName { get; set; }

        public string Description { get; set; }

        public decimal Amount { get; set; }

        public string Folder { get; set; }

        public string ConsecutiveNumber { get; set; }
    }
}
