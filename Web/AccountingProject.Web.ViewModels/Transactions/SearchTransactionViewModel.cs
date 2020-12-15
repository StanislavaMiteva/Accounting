namespace AccountingProject.Web.ViewModels.Transactions
{
    using System.Collections.Generic;

    using AccountingProject.Web.ViewModels.Counterparties;
    using AccountingProject.Web.ViewModels.DocumentTypes;
    using AccountingProject.Web.ViewModels.Identity;

    public class SearchTransactionViewModel : SearchInputModel
    {
        public IEnumerable<CounterpartyPartViewModel> Counterparties { get; set; }

        public IEnumerable<DocumentTypePartViewModel> Documents { get; set; }

        public IEnumerable<UserViewModel> Users { get; set; }
    }
}
