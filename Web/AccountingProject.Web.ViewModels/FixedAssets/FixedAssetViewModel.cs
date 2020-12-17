namespace AccountingProject.Web.ViewModels.FixedAssets
{
    using System;

    using AccountingProject.Data.Models;

    public class FixedAssetViewModel : FixedAssetInListViewModel
    {
        public string InventoryNumber { get; set; }

        public DateTime? DerecognitionDate { get; set; }

        public string DerecognitionDateAsString => this.DerecognitionDate?.ToString("dd/MM/yyyy");

        public DepreciationMethod DepreciationMethod { get; set; }

        public string AccountablePerson { get; set; }

        public decimal BookValue => this.Amount - this.DepreciationExpense;

        public string BookValueAsString => this.BookValue.ToString("F2");
    }
}
