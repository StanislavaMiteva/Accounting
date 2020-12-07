namespace AccountingProject.Web.ViewModels.ViewComponents
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AccountingProject.Web.ViewModels.GLAccounts;

    public class ListOfMainAccountsViewModel
    {
        public ListOfMainAccountsViewModel()
        {
            this.MainAccounts = new HashSet<MainAccountPartViewModel>();
        }

        [Display(Name = "Main Account")]
        public int MainAccountId { get; set; }

        public string TypeOfAccount { get; set; }

        public IEnumerable<MainAccountPartViewModel> MainAccounts { get; set; }
    }
}
