namespace AccountingProject.Web.ViewModels.FixedAssets
{
    using System;

    using AccountingProject.Common;
    using AccountingProject.Data.Models;
    using AccountingProject.Services.Mapping;

    public class FixedAssetInListViewModel : IMapFrom<FixedAsset>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public decimal AcquisitionPrice { get; set; }

        public DateTime AcquisitionDate { get; set; }

        public string AcquisitionDateAsString => this.AcquisitionDate.ToString("dd/MM/yyyy");

        public int GLAccountCode { get; set; }

        public string GLAccountName { get; set; }

        public decimal Amount => (decimal)this.Quantity * this.AcquisitionPrice;

        public int UsefulLife { get; set; }

        public decimal SalvageValue { get; set; }

        public decimal DepreciationExpense => (this.Amount - this.SalvageValue) / this.UsefulLife / GlobalConstants.MonthsPerYear * (decimal)this.MonthsUsage;

        public string DepreciationExpenseAsString => this.DepreciationExpense.ToString("F2");

        public double MonthsUsage => Math.Floor(DateTime.UtcNow.Subtract(this.AcquisitionDate).Days / GlobalConstants.DaysPerMonth);
    }
}
