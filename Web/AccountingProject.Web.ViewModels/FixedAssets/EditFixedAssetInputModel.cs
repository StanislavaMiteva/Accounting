namespace AccountingProject.Web.ViewModels.FixedAssets
{
    using AccountingProject.Data.Models;
    using AccountingProject.Services.Mapping;
    using AutoMapper;

    public class EditFixedAssetInputModel :
        CreateFixedAssetInputModel,
        IMapFrom<FixedAsset>,
        IHaveCustomMappings
    {
        public int Id { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<FixedAsset, EditFixedAssetInputModel>()
               .ForMember(x => x.MainAccountId, opt =>
                   opt.MapFrom(x => x.GLAccountId));
        }
    }
}
