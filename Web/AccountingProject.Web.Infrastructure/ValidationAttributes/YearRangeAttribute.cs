namespace AccountingProject.Web.Infrastructure.ValidationAttributes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class YearRangeAttribute : ValidationAttribute
    {
        public YearRangeAttribute(int minYear)
        {
            this.MinYear = minYear;
            this.ErrorMessage = $"Year should be between {minYear} and {DateTime.UtcNow.Year}.";
        }

        public int MinYear { get; }

        public override bool IsValid(object value)
        {
            if (value is int year)
            {
                if (year <= DateTime.UtcNow.Year
                    && year >= this.MinYear)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
