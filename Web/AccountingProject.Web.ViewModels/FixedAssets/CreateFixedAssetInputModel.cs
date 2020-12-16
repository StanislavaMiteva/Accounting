namespace AccountingProject.Web.ViewModels.FixedAssets
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using AccountingProject.Common;
    using AccountingProject.Data.Models;
    using AccountingProject.Web.Infrastructure.ValidationAttributes;
    using AccountingProject.Web.ViewModels.ViewComponents;

    public class CreateFixedAssetInputModel
    {
        [Required]
        [MinLength(2, ErrorMessage = "The Name must be between 2 and 100 characters long.")]
        [MaxLength(100, ErrorMessage = "The Name must be between 2 and 100 characters long.")]
        public string Name { get; set; }

        [Display(Name= "Inventory Number")]
        [MinLength(2, ErrorMessage = "The inventory number must be between 2 and 20 characters long.")]
        [MaxLength(20, ErrorMessage = "The inventory number must be between 2 and 20 characters long.")]
        public string InventoryNumber { get; set; }

        [Range(1, double.MaxValue)]
        public int Quantity { get; set; }

        [Display(Name = "Acquisition Price")]
        [Range(typeof(decimal), GlobalConstants.MinPrice, GlobalConstants.MaxDecimalValue)]
        public decimal AcquisitionPrice { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Acquisition Date")]
        [CurrentDateMaxValue(GlobalConstants.MinimumAllowedYearForDocumentDate)]
        public DateTime AcquisitionDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Derecognition Date")]
        public DateTime? DerecognitionDate { get; set; }

        [Display(Name = "Useful Life")]
        [Range(GlobalConstants.MinUsefulLife, GlobalConstants.MaxUsefulLife)]
        public int UsefulLife { get; set; }

        [Display(Name = "Salvage Value")]
        [Range(typeof(decimal), GlobalConstants.MinSalvageValue, GlobalConstants.MaxDecimalValue)]
        public decimal SalvageValue { get; set; }

        [Display(Name = "Depreciation Method")]
        [AllowedDepreciationMethod]
        public DepreciationMethod DepreciationMethod { get; set; }

        [Display(Name= "Accountable Person")]
        [MinLength(2, ErrorMessage = "The name must be between 2 and 50 characters long.")]
        [MaxLength(50, ErrorMessage = "The name must be between 2 and 50 characters long.")]
        public string AccountablePerson { get; set; }

        public int MainAccountId { get; set; }

        public ListOfMainAccountsViewModel ListOfMainAccounts { get; set; }
    }
}
