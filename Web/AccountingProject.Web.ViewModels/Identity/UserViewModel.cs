namespace AccountingProject.Web.ViewModels.Identity
{
    using System;
    using System.Collections.Generic;

    using AccountingProject.Data.Models;
    using AccountingProject.Services.Mapping;

    public class UserViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public DateTime? DeletedOn { get; set; }

        public IEnumerable<RoleNameIdViewModel> Roles { get; set; }
    }
}
