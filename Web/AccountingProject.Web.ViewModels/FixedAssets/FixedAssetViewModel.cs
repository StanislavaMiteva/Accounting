namespace AccountingProject.Web.ViewModels.FixedAssets
{
    using System;

    using AccountingProject.Common;
    using AccountingProject.Data.Models;

    public class FixedAssetViewModel : FixedAssetInListViewModel
    {
        public DateTime? DerecognitionDate { get; set; }

        public string DerecognitionDateAsString => this.DerecognitionDate?.ToString("dd/MM/yyyy");

        public int UsefulLife { get; set; }

        public decimal SalvageValue { get; set; }

        public DepreciationMethod DepreciationMethod { get; set; }

        public string AccountablePerson { get; set; }

        public decimal DepreciationExpense => (this.Amount - this.SalvageValue) / this.UsefulLife / GlobalConstants.MonthsPerYear * (decimal)this.MonthsUsage;

        public string DepreciationExpenseAsString => this.DepreciationExpense.ToString("F2");

        public decimal BookValue => this.Amount - this.DepreciationExpense;

        public string BookValueAsString => this.BookValue.ToString("F2");

        public double MonthsUsage => Math.Floor(DateTime.UtcNow.Subtract(this.AcquisitionDate).Days / GlobalConstants.DaysPerMonth);
    }
}
