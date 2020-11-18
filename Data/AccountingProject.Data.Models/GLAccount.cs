namespace AccountingProject.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AccountingProject.Data.Common.Models;

    public class GLAccount : BaseDeletableModel<int>
    {
        public GLAccount()
        {
            this.DebitTransactions = new HashSet<Transaction>();
            this.CreditTransactions = new HashSet<Transaction>();
            this.AnalyticalAccounts = new HashSet<AnalyticalAccount>();
            this.Inventories = new HashSet<Inventory>();
            this.FixedAssets = new HashSet<FixedAsset>();

            // this.IsFixedAsset = false;
            // this.IsInventory = false;
        }

        public int Code { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        public decimal DebitBalance { get; set; }

        public decimal CreditBalance { get; set; }

        public bool IsInventory { get; set; }

        public bool IsFixedAsset { get; set; }

        public virtual ICollection<Transaction> DebitTransactions { get; set; }

        public virtual ICollection<Transaction> CreditTransactions { get; set; }

        public virtual ICollection<AnalyticalAccount> AnalyticalAccounts { get; set; }

        public virtual ICollection<Inventory> Inventories { get; set; }

        public virtual ICollection<FixedAsset> FixedAssets { get; set; }
    }
}
