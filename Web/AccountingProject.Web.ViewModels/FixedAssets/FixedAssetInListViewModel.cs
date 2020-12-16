namespace AccountingProject.Web.ViewModels.FixedAssets
{
    using System;

    using AccountingProject.Data.Models;
    using AccountingProject.Services.Mapping;

    public class FixedAssetInListViewModel : IMapFrom<FixedAsset>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string InventoryNumber { get; set; }

        public int Quantity { get; set; }

        public decimal AcquisitionPrice { get; set; }

        public DateTime AcquisitionDate { get; set; }

        public string AcquisitionDateAsString => this.AcquisitionDate.ToString("dd/MM/yyyy");

        public int GLAccountCode { get; set; }

        public string GLAccountName { get; set; }

        public decimal Amount => (decimal)this.Quantity * this.AcquisitionPrice;
    }
}
