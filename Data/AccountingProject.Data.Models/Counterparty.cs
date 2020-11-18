namespace AccountingProject.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AccountingProject.Data.Common.Models;

    public class Counterparty : BaseDeletableModel<int>
    {
        public Counterparty()
        {
            this.Transactions = new HashSet<Transaction>();
        }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(20)]
        public string VAT { get; set; }

        [MaxLength(200)]
        public string Address { get; set; }

        public int CityId { get; set; }

        public virtual City City { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
