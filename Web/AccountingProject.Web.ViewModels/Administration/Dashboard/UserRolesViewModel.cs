namespace AccountingProject.Web.ViewModels.Administration.Dashboard
{
    using System.Collections.Generic;

    using AccountingProject.Web.ViewModels.Identity;

    public class UserRolesViewModel
    {
        public string UserName { get; set; }

        public IEnumerable<RoleNameIdViewModel> Roles { get; set; }
    }
}
