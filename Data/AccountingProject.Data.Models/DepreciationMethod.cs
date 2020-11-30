namespace AccountingProject.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public enum DepreciationMethod
    {
        [Display(Name= "Straight Line")]
        StraightLine = 1,
        DoubleDecliningBalance = 2,
        UnitsOfProduction = 3,
        SumOfYearsDigits = 4,
    }
}
