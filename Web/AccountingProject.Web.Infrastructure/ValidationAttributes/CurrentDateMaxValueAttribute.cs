namespace AccountingProject.Web.Infrastructure.ValidationAttributes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CurrentDateMaxValueAttribute : ValidationAttribute
    {
        public CurrentDateMaxValueAttribute(int minYear)
        {
            this.MinYear = minYear;
            this.ErrorMessage = $"Date should be between 01.01.{minYear} and {DateTime.UtcNow:dd/MM/yyyy}.";
        }

        public int MinYear { get; }

        public override bool IsValid(object value)
        {
            if (value is DateTime date)
            {
                if (date <= DateTime.UtcNow.Date
                    && date.Year >= this.MinYear)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
