namespace AccountingProject.Web.ViewModels.ViewComponents
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ListOfMainAccountsViewModel
    {
        public ListOfMainAccountsViewModel()
        {
            this.MainAccounts = new HashSet<KeyValuePair<string, string>>();
        }

        public string TypeOfAccount { get; set; }

        [Display(Name = "Main Account")]
        public int MainAccountId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> MainAccounts { get; set; }
    }
}
