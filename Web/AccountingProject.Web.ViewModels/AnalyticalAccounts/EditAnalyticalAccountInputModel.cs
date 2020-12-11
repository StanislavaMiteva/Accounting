namespace AccountingProject.Web.ViewModels.AnalyticalAccounts
{
    using System.ComponentModel.DataAnnotations;

    using AccountingProject.Data.Models;
    using AccountingProject.Services.Mapping;

    public class EditAnalyticalAccountInputModel :
        BaseAnalyticalAccountInputModel,
        IMapFrom<AnalyticalAccount>,
        IHaveCustomMappings
    {
        public int Id { get; set; }

        [Display(Name = "Main Account")]
        public string MainAccount { get; set; }

        public void CreateMappings(AutoMapper.IProfileExpression configuration)
        {
            configuration.CreateMap<AnalyticalAccount, EditAnalyticalAccountInputModel>()
                .ForMember(x => x.MainAccount, opt =>
                    opt.MapFrom(x => $"{x.GlAccount.Code} {x.GlAccount.Name}"))
                .ForMember(x => x.MainAccountId, opt =>
                    opt.MapFrom(x => x.GLAccountId));
        }
    }
}
