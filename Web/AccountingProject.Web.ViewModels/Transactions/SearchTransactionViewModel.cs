namespace AccountingProject.Web.ViewModels.Transactions
{
    using System.Collections.Generic;

    using AccountingProject.Data.Models;
    using AccountingProject.Web.ViewModels.Counterparties;
    using AccountingProject.Web.ViewModels.DocumentTypes;

    public class SearchTransactionViewModel : SearchInputModel
    {
        public IEnumerable<CounterpartyPartViewModel> Counterparties { get; set; }

        public IEnumerable<DocumentTypePartViewModel> Documents { get; set; }

        public IEnumerable<ApplicationUser> Users { get; set; }
    }
}
