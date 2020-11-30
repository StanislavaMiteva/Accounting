namespace AccountingProject.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using AccountingProject.Data.Common.Models;

    public class Transaction : BaseDeletableModel<string>
    {
        public Transaction()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public DateTime DocumentDate { get; set; }

        public int DocumentTypeId { get; set; }

        public virtual DocumentType DocumentType { get; set; }

        [Required]
        [MaxLength(120)]
        public string Description { get; set; }

        public int DebitGLAccountId { get; set; }

        public virtual GLAccount DebitGLAccount { get; set; }

        public int? DebitAnalyticalAccountId { get; set; }

        public virtual AnalyticalAccount DebitAnalyticalAccount { get; set; }

        public int CreditGLAccountId { get; set; }

        public virtual GLAccount CreditGLAccount { get; set; }

        public int? CreditAnalyticalAccountId { get; set; }

        public virtual AnalyticalAccount CreditAnalyticalAccount { get; set; }

        public decimal Amount { get; set; }

        public int? CounterpartyId { get; set; }

        public virtual Counterparty Counterparty { get; set; }

        [Required]
        public string CreatorId { get; set; }

        public virtual ApplicationUser Creator { get; set; }

        public bool IsPurchase { get; set; }

        public bool IsSale { get; set; }

        [MaxLength(10)]
        public string Folder { get; set; }

        [MaxLength(10)]
        public string ConsecutiveNumber { get; set; }
    }
}
