namespace AccountingProject.Web.ViewModels.Identity
{
    using AccountingProject.Data.Models;
    using AccountingProject.Services.Mapping;

    public class RoleNameIdViewModel : IMapFrom<ApplicationRole>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
