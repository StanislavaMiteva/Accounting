namespace AccountingProject.Web.ViewModels.AnalyticalAccounts
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AccountingProject.Common;
    using AccountingProject.Data.Models;
    using AccountingProject.Services.Mapping;

    public class EditAnalyticalAccountInputModel :
        BaseAnalyticalAccountInputModel,
        IMapFrom<AnalyticalAccount>,
        IValidatableObject,
        IHaveCustomMappings
    {
        public int Id { get; set; }

        [Display(Name = "Debit Balance")]
        [Range(typeof(decimal), GlobalConstants.MinAccountBalance, GlobalConstants.MaxDecimalValue)]
        public decimal DebitBalance { get; set; }

        [Display(Name = "Credit Balance")]
        [Range(typeof(decimal), GlobalConstants.MinAccountBalance, GlobalConstants.MaxDecimalValue)]
        public decimal CreditBalance { get; set; }

        public IEnumerable<KeyValuePair<string, string>> MainAccounts { get; set; }

        public void CreateMappings(AutoMapper.IProfileExpression configuration)
        {
            configuration.CreateMap<AnalyticalAccount, EditAnalyticalAccountInputModel>()
                .ForMember(x => x.MainAccountId, opt =>
                    opt.MapFrom(x => x.GLAccountId));
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.DebitBalance > 0 && this.CreditBalance > 0)
            {
                yield return new ValidationResult("Balance must be either debit or credit.");
            }
        }
    }
}
