namespace AccountingProject.Web.ViewModels.GLAccounts
{
    using System.ComponentModel.DataAnnotations;
    

    public class CreateMainAccountInputModel
    {
        [Range(100, 999)]
        public int Code { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(200)]
        //[MainAccountNameIsAvailableAttribute]
        public string Name { get; set; }

        [Display(Name = "It's an inventory account")]
        public bool IsInventory { get; set; }

        [Display(Name = "It's a fixed asset account")]
        public bool IsFixedAsset { get; set; }
    }
}
