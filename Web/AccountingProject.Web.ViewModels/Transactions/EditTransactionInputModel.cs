namespace AccountingProject.Web.ViewModels.Transactions
{
    using AccountingProject.Data.Models;
    using AccountingProject.Services.Mapping;
    using AutoMapper;

    public class EditTransactionInputModel :
        BaseTransactionInputModel,
        IMapFrom<GLAccount>,
        IHaveCustomMappings
    {
        public string Id { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Transaction, EditTransactionInputModel>()
               .ForMember(x => x.DebitMainAccountId, opt =>
                   opt.MapFrom(x => x.DebitGLAccountId))
               .ForMember(x => x.CreditMainAccountId, opt =>
                   opt.MapFrom(x => x.CreditGLAccountId));
        }
    }
}
