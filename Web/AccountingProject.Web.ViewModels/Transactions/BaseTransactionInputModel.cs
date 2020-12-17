namespace AccountingProject.Web.ViewModels.Transactions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AccountingProject.Common;
    using AccountingProject.Web.Infrastructure.ValidationAttributes;
    using AccountingProject.Web.ViewModels.AnalyticalAccounts;
    using AccountingProject.Web.ViewModels.Counterparties;
    using AccountingProject.Web.ViewModels.DocumentTypes;

    public abstract class BaseTransactionInputModel : IValidatableObject
    {
        [Display(Name = "Date of the document")]
        [DataType(DataType.Date)]
        [CurrentDateMaxValue(GlobalConstants.MinimumAllowedYearForDocumentDate)]
        public DateTime DocumentDate { get; set; }

        [Display(Name = "Type of the document")]
        public int DocumentTypeId { get; set; }

        [Display(Name = "Debit Main Account")]
        public int DebitMainAccountId { get; set; }

        [Display(Name = "Debit Analytical Account")]
        public int? DebitAnalyticalAccountId { get; set; }

        public string DebitAnalyticalAccountName { get; set; }

        [Display(Name = "Credit Main Account")]
        public int CreditMainAccountId { get; set; }

        [Display(Name = "Credit Analytical Account")]
        public int? CreditAnalyticalAccountId { get; set; }

        public string CreditAnalyticalAccountName { get; set; }

        [Display(Name = "Counterparty Name")]
        public int CounterpartyId { get; set; }

        [Display(Name = "It' a purchase")]
        public bool IsPurchase { get; set; }

        [Display(Name = "It's a sale")]
        public bool IsSale { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "The field Description must be between 5 and 120 characters long.")]
        [MaxLength(120, ErrorMessage = "The field Description must be between 5 and 120 characters long.")]
        public string Description { get; set; }

        [MaxLength(10)]
        public string Folder { get; set; }

        [MaxLength(10)]
        [Display(Name = "Consecutive Number")]
        public string ConsecutiveNumber { get; set; }

        [Range(typeof(decimal), GlobalConstants.MinPrice, GlobalConstants.MaxDecimalValue)]
        public decimal Amount { get; set; }

        public IEnumerable<DocumentTypePartViewModel> Documents { get; set; }

        public IEnumerable<KeyValuePair<string, string>> MainAccounts { get; set; }

        public IEnumerable<AnalyticalAccountPartViewModel> AnalyticalAccounts { get; set; }

        public IEnumerable<CounterpartyPartViewModel> Counterparties { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.IsPurchase == true && this.IsSale == true)
            {
                yield return new ValidationResult("Transaction must be either purchase or sale or none of the two.");
            }
        }
    }
}
