namespace AccountingProject.Web.ViewModels.Identity
{
    using AccountingProject.Data.Models;
    using AccountingProject.Services.Mapping;

    public class UserNameIdViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string UserName { get; set; }
    }
}
