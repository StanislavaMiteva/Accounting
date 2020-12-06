namespace AccountingProject.Web.ViewModels.Inventories
{
    using System.Collections.Generic;
    using System.Linq;

    public class InventoriesListViewModel
    {
        public IEnumerable<InventoryViewModel> Inventories { get; set; }

        public decimal TotalAmount => this.Inventories.Sum(x => x.Amount);
    }
}
