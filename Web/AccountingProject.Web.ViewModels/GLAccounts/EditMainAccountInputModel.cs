namespace AccountingProject.Web.ViewModels.GLAccounts
{
    using AccountingProject.Data.Models;
    using AccountingProject.Services.Mapping;

    public class EditMainAccountInputModel :
        CreateMainAccountInputModel,
        IMapFrom<GLAccount>
    {
        public int Id { get; set; }
    }
}
