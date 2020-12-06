namespace AccountingProject.Web.ViewModels.AnalyticalAccounts
{
    using System.ComponentModel.DataAnnotations;

    using AccountingProject.Web.ViewModels.ViewComponents;

    public class CreateAnalyticalAccountInputModel
    {
        [Required]
        [MinLength(2)]
        [MaxLength(200)]
        public string Name { get; set; }

        public ListOfMainAccountsViewModel ListOfMainAccounts { get; set; }

        public int MainAccountId { get; set; }
    }
}
