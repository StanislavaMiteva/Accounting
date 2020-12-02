namespace AccountingProject.Web.ViewModels.GLAccounts
{
    using AccountingProject.Data.Models;
    using AccountingProject.Services.Mapping;

    public class MainAccountViewModel : IMapFrom<GLAccount>
    {
        public int Id { get; set; }

        public int Code { get; set; }

        public string Name { get; set; }

        public decimal DebitBalance { get; set; }

        public decimal CreditBalance { get; set; }

        public bool IsInventory { get; set; }

        public bool IsFixedAsset { get; set; }
    }
}
