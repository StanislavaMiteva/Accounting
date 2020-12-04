﻿namespace AccountingProject.Web.ViewModels.GLAccounts
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AccountingProject.Common;

    public class AddAccountBalanceInputModel : IValidatableObject
    {
        [Display(Name = "Main Account")]
        public int DebitMainAccountId { get; set; }

        [Display(Name = "Analytical Account")]
        public int? AnalyticalAccountId { get; set; }

        public string AnalyticalAccountName { get; set; }

        [Display(Name = "Debit Balance")]
        [Range(typeof(decimal), GlobalConstants.MinAccountBalance, GlobalConstants.MaxDecimalValue)]
        public decimal DebitBalance { get; set; }

        [Display(Name = "Credit Balance")]
        [Range(typeof(decimal), GlobalConstants.MinAccountBalance, GlobalConstants.MaxDecimalValue)]
        public decimal CreditBalance { get; set; }

        public IEnumerable<MainAccountPartViewModel> MainAccounts { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.DebitBalance > 0 && this.CreditBalance > 0)
            {
                yield return new ValidationResult("Balance must be either debit or credit.");
            }

            if (this.DebitBalance == 0 && this.CreditBalance == 0)
            {
                yield return new ValidationResult("Please insert either debit or credit balance.");
            }
        }
    }
}
