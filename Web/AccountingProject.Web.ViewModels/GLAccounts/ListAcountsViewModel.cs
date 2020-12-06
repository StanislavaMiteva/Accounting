namespace AccountingProject.Web.ViewModels.GLAccounts
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ListAcountsViewModel
    {
        [Display(Name = "Main Account")]
        public int MainAccountId { get; set; }

        public IEnumerable<MainAccountPartViewModel> MainAccounts { get; set; }
    }
}
