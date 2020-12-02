namespace AccountingProject.Web.ViewModels.Shared
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AccountingProject.Common;
    using AccountingProject.Web.Infrastructure.ValidationAttributes;

    public class InputYearMonthModel : IValidatableObject
    {
        [YearRange(GlobalConstants.MinimumAllowedYearForDocumentDate)]
        public int Year { get; set; }

        [Range(1, 12)]
        [Display(Name = "Start Month")]
        public int MonthStart { get; set; }

        [Range(1, 12)]
        [Display(Name = "End Month")]
        public int MonthEnd { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.MonthStart > this.MonthEnd)
            {
                yield return new ValidationResult("Start month must be bigger than end month.");
            }
        }
    }
}
