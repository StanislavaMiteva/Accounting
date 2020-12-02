namespace AccountingProject.Web.ViewModels.AnalyticalAccounts
{
    using AccountingProject.Data.Models;
    using AccountingProject.Services.Mapping;

    public class AnalyticalAccountPartViewModel : IMapFrom<AnalyticalAccount>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
