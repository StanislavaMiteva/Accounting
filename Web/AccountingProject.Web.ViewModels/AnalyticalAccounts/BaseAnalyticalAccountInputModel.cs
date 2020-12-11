namespace AccountingProject.Web.ViewModels.AnalyticalAccounts
{
    using System.ComponentModel.DataAnnotations;

    public abstract class BaseAnalyticalAccountInputModel
    {
        [Required]
        [MinLength(2)]
        [MaxLength(200)]
        public string Name { get; set; }

        [Display(Name = "Main Account")]
        public int MainAccountId { get; set; }
    }
}
