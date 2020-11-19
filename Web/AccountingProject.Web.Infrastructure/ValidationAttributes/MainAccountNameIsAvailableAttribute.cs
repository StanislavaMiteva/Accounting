namespace AccountingProject.Web.Infrastructure.ValidationAttributes
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using AccountingProject.Services.Data;

    public class MainAccountNameIsAvailableAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var service = (IGLAccountsService)validationContext
                         .GetService(typeof(IGLAccountsService));
            var account = service.GetAllOnlyIdCodeName()
                .FirstOrDefault(x => x.Name == value.ToString());

            if (account != null)
            {
                return new ValidationResult("error");
            }

            return ValidationResult.Success;
        }
    }
}
