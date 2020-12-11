namespace AccountingProject.Web.ViewModels.Counterparties
{
    using AccountingProject.Data.Models;
    using AccountingProject.Services.Mapping;

    public class EditCounterpartyInputModel :
        BaseCounterpartyInputModel,
        IMapFrom<Counterparty>
    {
        public int Id { get; set; }
    }
}
