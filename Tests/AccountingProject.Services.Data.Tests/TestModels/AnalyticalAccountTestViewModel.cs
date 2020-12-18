namespace AccountingProject.Services.Data.Tests.TestModels
{
    using AccountingProject.Data.Models;
    using AccountingProject.Services.Mapping;

    public class AnalyticalAccountTestViewModel : IMapTo<AnalyticalAccount>, IMapFrom<AnalyticalAccount>
    {
        public int Id { get; set; }

        public string Name { get; set; }


    }
}
