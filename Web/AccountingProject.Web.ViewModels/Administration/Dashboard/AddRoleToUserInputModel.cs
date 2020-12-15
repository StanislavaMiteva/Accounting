namespace AccountingProject.Web.ViewModels.Administration.Dashboard
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AccountingProject.Web.ViewModels.Identity;

    public class AddRoleToUserInputModel
    {
        [Display(Name = "User name")]
        public string UserId { get; set; }

        [Display(Name = "Role name")]
        public string RoleId { get; set; }

        public IEnumerable<UserNameIdViewModel> Users { get; set; }

        public IEnumerable<RoleNameIdViewModel> Roles { get; set; }
    }
}
