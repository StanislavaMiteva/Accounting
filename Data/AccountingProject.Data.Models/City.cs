namespace AccountingProject.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AccountingProject.Data.Common.Models;

    public class City : BaseDeletableModel<int>
    {
        public City()
        {
            this.Counterparties = new HashSet<Counterparty>();
        }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Counterparty> Counterparties { get; set; }
    }
}
