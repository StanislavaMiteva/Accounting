namespace AccountingProject.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public enum DepreciationMethod
    {
        [Display(Name= "Straight Line")]
        StraightLine = 1,
        [Display(Name = "Double Declining Balance")]
        DoubleDecliningBalance = 2,
        [Display(Name = "Units Of Production")]
        UnitsOfProduction = 3,
        [Display(Name = "Sum Of Years Digits")]
        SumOfYearsDigits = 4,
    }
}
