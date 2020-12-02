namespace AccountingProject.Web.ViewModels.Counterparties
{
    using AccountingProject.Data.Models;
    using AccountingProject.Services.Mapping;

    public class CounterpartyViewModel : IMapFrom<Counterparty>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string VAT { get; set; }

        public string Address { get; set; }

        public string CityName { get; set; }
    }
}
