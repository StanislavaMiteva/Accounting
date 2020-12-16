namespace AccountingProject.Web.Infrastructure.ValidationAttributes
{
    using System.ComponentModel.DataAnnotations;

    using AccountingProject.Data.Models;

    public class AllowedDepreciationMethod : ValidationAttribute
    {
        public AllowedDepreciationMethod()
        {
            this.ErrorMessage = $"Straight line method is the only one that can be used so far.";
        }

        public override bool IsValid(object value)
        {
            if (value is DepreciationMethod method)
            {
                if (method == DepreciationMethod.StraightLine)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
