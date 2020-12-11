namespace AccountingProject.Web.ViewModels.Inventories
{
    using AccountingProject.Data.Models;
    using AccountingProject.Services.Mapping;
    using AutoMapper;

    public class EditInventoryInputModel :
        CreateInventoryInputModel,
        IMapFrom<Inventory>,
        IHaveCustomMappings
    {
        public int Id { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Inventory, EditInventoryInputModel>()
               .ForMember(x => x.MainAccountId, opt =>
                   opt.MapFrom(x => x.GLAccountId));
        }
    }
}
