namespace AccountingProject.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using AccountingProject.Data.Common.Models;

    public class FixedAsset : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(20)]
        public string InventoryNumber { get; set; }

        public int Quantity { get; set; }

        public decimal AcquisitionPrice { get; set; }

        public DateTime AcquisitionDate { get; set; }

        public DateTime DerecognitionDate { get; set; }

        public int UsefulLife { get; set; }

        public decimal SalvageValue { get; set; }

        public DepreciationMethod DepreciationMethod { get; set; }

        [MaxLength(50)]
        public string AccountablePerson { get; set; }

        public int GLAccountId { get; set; }

        public virtual GLAccount GLAccount { get; set; }
    }
}
