namespace AccountingProject.Web.ViewModels.Inventories
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using AccountingProject.Common;
    using AccountingProject.Web.ViewModels.ViewComponents;

    public class CreateInventoryInputModel
    {
        [Required]
        [MinLength(2, ErrorMessage = "The Name must be between 2 and 30 characters long.")]
        [MaxLength(30, ErrorMessage = "The Name must be between 2 and 30 characters long.")]
        public string Name { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "The field Measure must be between 1 and 10 characters long.")]
        [MaxLength(10, ErrorMessage = "The field Measure must be between 1 and 10 characters long.")]
        public string Measure { get; set; }

        [Range(1, double.MaxValue)]
        public double Quantity { get; set; }

        [Range(typeof(decimal), GlobalConstants.MinPrice, GlobalConstants.MaxDecimalValue)]
        public decimal Price { get; set; }

        public ListOfMainAccountsViewModel ListOfMainAccounts { get; set; }

        public int MainAccountId { get; set; }
    }
}
