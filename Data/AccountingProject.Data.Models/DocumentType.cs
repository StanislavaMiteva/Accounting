namespace AccountingProject.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AccountingProject.Data.Common.Models;

    public class DocumentType : BaseDeletableModel<int>
    {
        public DocumentType()
        {
            this.Transactions = new HashSet<Transaction>();
        }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
