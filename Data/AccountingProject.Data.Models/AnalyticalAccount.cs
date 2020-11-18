namespace AccountingProject.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using AccountingProject.Data.Common.Models;

    public class AnalyticalAccount : BaseDeletableModel<int>
    {
        public AnalyticalAccount()
        {
            this.DebitTransactions = new HashSet<Transaction>();
            this.CreditTransactions = new HashSet<Transaction>();
        }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [ForeignKey(nameof(GLAccount))]
        public int GLAccountId { get; set; }

        public virtual GLAccount GlAccount { get; set; }

        public decimal DebitBalance { get; set; }

        public decimal CreditBalance { get; set; }

        public virtual ICollection<Transaction> DebitTransactions { get; set; }

        public virtual ICollection<Transaction> CreditTransactions { get; set; }
    }
}
