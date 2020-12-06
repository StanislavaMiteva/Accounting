namespace AccountingProject.Web.ViewModels.Inventories
{
    using AccountingProject.Data.Models;
    using AccountingProject.Services.Mapping;

    public class InventoryViewModel : IMapFrom<Inventory>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Measure { get; set; }

        public double Quantity { get; set; }

        public decimal Price { get; set; }

        public int GLAccountCode { get; set; }

        public string GLAccountName { get; set; }

        public decimal Amount => (decimal)this.Quantity * this.Price;
    }
}
