namespace AccountingProject.Web.ViewModels.AnalyticalAccounts
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AccountingProject.Web.ViewModels.GLAccounts;

    public class CreateAnalyticalAccountInputModel
    {
        [Required]
        [MinLength(2)]
        [MaxLength(200)]
        public string Name { get; set; }

        [Display(Name = "Main Account")]
        public int MainAccountId { get; set; }

        public IEnumerable<MainAccountPartViewModel> MainAccounts { get; set; }
    }
}
