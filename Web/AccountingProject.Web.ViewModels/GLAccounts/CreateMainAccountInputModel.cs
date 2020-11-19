namespace AccountingProject.Web.ViewModels.GLAccounts
{
    using System.ComponentModel.DataAnnotations;

    // using AccountingProject.Web.Infrastructure.ValidationAttributes;
    public class CreateMainAccountInputModel
    {
        [Range(100, 999)]
        public int Code { get; set; }

        // [MainAccountNameIsAvailable]
        [Required]
        [MinLength(2, ErrorMessage = "The field Name must be between 2 and 200 characters long.")]
        [MaxLength(200, ErrorMessage = "The field Name must be between 2 and 200 characters long.")]
        public string Name { get; set; }

        [Display(Name = "It's an inventory account")]
        public bool IsInventory { get; set; }

        [Display(Name = "It's a fixed asset account")]
        public bool IsFixedAsset { get; set; }
    }
}
