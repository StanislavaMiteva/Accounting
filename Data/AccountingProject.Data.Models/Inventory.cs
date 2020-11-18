namespace AccountingProject.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using AccountingProject.Data.Common.Models;

    public class Inventory : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MaxLength(10)]
        public string Measure { get; set; }

        public double Quantity { get; set; }

        public decimal Price { get; set; }

        public int GLAccountId { get; set; }

        public virtual GLAccount GLAccount { get; set; }
    }
}
